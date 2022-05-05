using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unipply_Recommendations.DataStructures;

namespace Unipply_Recommendations.Services
{
    public class FacultyRecommender
    {
        public static string BaseDataSetRelativePath = @"../../../Data";
        private static string TrainingDataRelativePathAmazon = $"{BaseDataSetRelativePath}/Hobbies_Faculties.txt";
        private static string TrainingDataLocationAmazon = GetAbsolutePath(TrainingDataRelativePathAmazon);


        public PredictionEngine<FacultyEntry, Faculty_prediction> predictionengine;

        public PredictionEngine<FacultyEntry, Faculty_prediction> LoadData()
        {
            //STEP 1: Create MLContext to be shared across the model creation workflow objects 
            MLContext mlContext = new MLContext();

            //STEP 2: Read the trained data using TextLoader by defining the schema for reading the product co-purchase dataset
            //        Do remember to replace amazon0302.txt with dataset from https://snap.stanford.edu/data/amazon0302.html
            var traindata = mlContext.Data.LoadFromTextFile(path: TrainingDataLocationAmazon,
                                                      columns: new[]
                                                                {
                                                                    new TextLoader.Column("Label", DataKind.Single, 0),
                                                                    new TextLoader.Column(name:nameof(FacultyEntry.HobbyID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(0) }, keyCount: new KeyCount(100)),
                                                                    new TextLoader.Column(name:nameof(FacultyEntry.FacultyID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(1) }, keyCount: new KeyCount(100))
                                                                },
                                                      hasHeader: true,
                                                      separatorChar: '\t');

            //STEP 3: Your data is already encoded so all you need to do is specify options for MatrxiFactorizationTrainer with a few extra hyperparameters
            //        LossFunction, Alpa, Lambda and a few others like K and C as shown below and call the trainer. 
            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = nameof(FacultyEntry.HobbyID);
            options.MatrixRowIndexColumnName = nameof(FacultyEntry.FacultyID);
            options.LabelColumnName = "Label";
            options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
            options.Alpha = 0.01;
            options.Lambda = 0.025;
            options.NumberOfIterations = 30;
            options.C = 0.00001;

            //Step 4: Call the MatrixFactorization trainer by passing options.
            var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

            //STEP 5: Train the model fitting to the DataSet
            //Please add Amazon0302.txt dataset from https://snap.stanford.edu/data/amazon0302.html to Data folder if FileNotFoundException is thrown.
            ITransformer model = est.Fit(traindata);

            //STEP 6: Create prediction engine and predict the score for Product 63 being co-purchased with Product 3.
            //        The higher the score the higher the probability for this particular productID being co-purchased 
            return mlContext.Model.CreatePredictionEngine<FacultyEntry, Faculty_prediction>(model);
        }

        public List<int> PredictFacultiesForHobby(int hobbieNumber, PredictionEngine<FacultyEntry, Faculty_prediction> predictionengine)
        {

            Faculty movieService = new Faculty();
            Hobby hobbieService = new Hobby();

            var top3 = (from m in Enumerable.Range(0, 100)
                        let p = predictionengine.Predict(
                           new FacultyEntry()
                           {
                               HobbyID = (uint)hobbieNumber,
                               FacultyID = (uint)m
                           })
                        orderby p.Score descending
                        select (HobbyID: m, Score: p.Score)).Take(3);
         
            return top3.Select(t => t.HobbyID).ToList();
        }

        public int PredictPrecentMatchForHobby(int hobbieNumber, int facultyNumber, PredictionEngine<FacultyEntry, Faculty_prediction> predictionengine)
        {
            var prediction = predictionengine.Predict(
                  new FacultyEntry()
                  {
                      HobbyID = (uint)hobbieNumber,
                      FacultyID = (uint)facultyNumber
                  });

            if (prediction.Score > 1)
            {
                return 100;
            }
            else if ((int)(prediction.Score * 100) < 0)
            {
                return 0;
            }
            else
            {
                return (int)(prediction.Score * 100);
            }
        }

        public static string GetAbsolutePath(string relativeDatasetPath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativeDatasetPath);

            return fullPath;
        }

    }


    public class Faculty_prediction
    {
        public float Score { get; set; }
    }

    public class FacultyEntry
    {
        [KeyType(count: 100)]
        public uint HobbyID { get; set; }

        [KeyType(count: 100)]
        public uint FacultyID { get; set; }
    }
}
