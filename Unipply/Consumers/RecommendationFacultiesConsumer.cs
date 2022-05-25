using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Context;
using Unipply.Models;
using Unipply.Models.Faculty;
using Unipply.Models.Recommendation;
using Unipply.Models.User;
using Unipply.Repositories;
using Unipply.Services;

namespace Unipply.Consumers
{
    public class RecommendationFacultiesConsumer :
        AbstractConsumer<RecommendationFaculties>
    {
       
        public RecommendationFacultiesConsumer(
             IServiceProvider serviceProvider
        ) : base(serviceProvider)
        {
            Queue = "api-service/recommendationFaculties";

            _routingKey = "api-recommendationFaculties";

            _exchange = "api-service";

            InitializeEventBus();
        }

        protected override void LogMessageReceived(
            RecommendationFaculties message
        )
        {
            var r = message;
        }

        protected override async Task RecieveRecommendationData(RecommendationFaculties recommendationFaculties)
        {
            var data = recommendationFaculties.RecommendationFacultiesData;
            using (var scope = _serviceProvider.CreateScope())
            {
                var userProfileDataService = scope.ServiceProvider.GetService<IUserProfileDataService>();
                var userDataService = scope.ServiceProvider.GetService<IUserDataService>();
                var facultiesService = scope.ServiceProvider.GetService<IFacultyDataService>();
                var specialtyDataService = scope.ServiceProvider.GetService<ISpecialtyDataService>();
                var facultyDataUserProfileDataRepository = scope.ServiceProvider.GetService<IFacultyDataUserProfileDataRepository>();

                var faculties = data.RecommendationModel.Select(x => facultiesService.GetFacultyByTitle(x.Title)).Where(x => x!= null);
               /* var recommendedFaculties = faculties.Select(faculty =>
                     new RecommendationFacultyData
                     {
                         Id = Guid.NewGuid(),
                         FacultyDataId = faculty.Id,
                         FacultyTitle = faculty.Title,
                     }).ToList();*/

                var user = await userDataService.FindUserByIdAsync(data.UserId);
                var userProfile = await userProfileDataService.FindUserProfileDataByUserIdAsync(data.UserId);
                if (user == null)
                {
                    return;
                }

                if (userProfile != null)
                {
                    var hobbies = data.RecommendationModel
                        .SelectMany(x => x.HobbiesData.Select(h => new HobbyModel
                          {
                            HobbyId = h.HobbyId,
                            Title = h.HobbyTitle,
                          }
                        ).Distinct()).Distinct().ToList();
                    userProfile.Hobbies = hobbies.GroupBy(x => x.HobbyId).Select(x => x.First()).ToList();
                    userProfile.Recommendations = data.RecommendationModel;
                    userProfile.FavouritesSpecialties = null;
                    await userProfileDataService.UpdateAsync(userProfile);
                }
                else
                {

                    await userProfileDataService.CreateAsync(
                     new UserProfileData
                     {
                         Id = Guid.NewGuid(),
                         UserDataId = user.Id,
                         Recommendations = data.RecommendationModel
                     });
                }

            }
        }
    }
}
