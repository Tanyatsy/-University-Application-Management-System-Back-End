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
    public class RecommendationHobbiesConsumer :
        AbstractConsumer<Hobby>
    {
       
        public RecommendationHobbiesConsumer(
             IServiceProvider serviceProvider
        ) : base(serviceProvider)
        {
            Queue = "api-service/recommendationHobbies";

            _routingKey = "api-recommendationHobbies";

            _exchange = "api-service";

            InitializeEventBus();
        }

        protected override void LogMessageReceived(
            Hobby message
        )
        {
            var r = message;
        }

        protected override async Task RecieveRecommendationData(Hobby recommendationHobbies)
        {
            var data = recommendationHobbies.HobbyData;
            using (var scope = _serviceProvider.CreateScope())
            {
                var userProfileDataService = scope.ServiceProvider.GetService<IUserProfileDataService>();
                var userDataService = scope.ServiceProvider.GetService<IUserDataService>();
                var facultiesService = scope.ServiceProvider.GetService<IFacultyDataService>();
                var specialtyDataService = scope.ServiceProvider.GetService<ISpecialtyDataService>();
                var facultyDataUserProfileDataRepository = scope.ServiceProvider.GetService<IFacultyDataUserProfileDataRepository>();

                var user = await userDataService.FindUserByIdAsync(data.UserId);
                var userProfile = await userProfileDataService.FindUserProfileDataByUserIdAsync(data.UserId);
                if (user == null)
                {
                    return;
                }

                if (userProfile != null)
                {
                    userProfile.Hobbies = data.Hobbies;
                    await userProfileDataService.UpdateHobbiesAsync(userProfile);
                }
                else {

                   await userProfileDataService.CreateAsync(
                    new UserProfileData
                    {
                        Id = Guid.NewGuid(),
                        UserDataId = user.Id,
                        Hobbies = data.Hobbies
                    });
                }
                
            }
        }
    }
}
