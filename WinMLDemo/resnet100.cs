// This file was automatically generated by VS extension Windows Machine Learning Code Generator v3
// from model file resnet100.onnx
// Warning: This file may get overwritten if you add add an onnx file with the same name
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.AI.MachineLearning;
namespace WinMLDemo
{
    
    public sealed class resnet100Input
    {
        public TensorFloat data; // shape(1,3,112,112)
    }
    
    public sealed class resnet100Output
    {
        public TensorFloat fc1; // shape(1,512)
    }
    
    public sealed class resnet100Model
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<resnet100Model> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            resnet100Model learningModel = new resnet100Model();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<resnet100Output> EvaluateAsync(resnet100Input input)
        {
            binding.Bind("data", input.data);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new resnet100Output();
            output.fc1 = result.Outputs["fc1"] as TensorFloat;
            return output;
        }
       
    }
}
