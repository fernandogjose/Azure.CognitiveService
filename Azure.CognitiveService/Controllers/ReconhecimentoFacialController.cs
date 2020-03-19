using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.CognitiveService.Controllers
{
    public class ReconhecimentoFacialController : Controller
    {
        private readonly IFaceClient _faceClient;

        public ReconhecimentoFacialController()
        {
            _faceClient = new FaceClient(new ApiKeyServiceClientCredentials("724be83162344809bcf2b0aab28ee9bd")) { Endpoint = "https://westus2.api.cognitive.microsoft.com/" };
        }

        public async Task<IActionResult> IndexAsync()
        {
            IList<DetectedFace> detectFaces = await DetectFaceExtractAsync(RecognitionModel.Recognition02).ConfigureAwait(true);
            return View(detectFaces);
        }

        public async Task<IList<DetectedFace>> DetectFaceExtractAsync(string recognitionModel)
        {
            string imageFileName = "https://www.sbcoaching.com.br/blog/wp-content/uploads/2018/10/dinamica-o-que-e-uma-grupo.jpg";
            IList<DetectedFace> detectedFaces;

            try
            {
                detectedFaces = await _faceClient.Face.DetectWithUrlAsync($"{imageFileName}",
                                   returnFaceAttributes: new List<FaceAttributeType>
                                   {
                                        FaceAttributeType.Accessories, FaceAttributeType.Age, FaceAttributeType.Blur, FaceAttributeType.Emotion,
                                        FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                                        FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                                        FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile
                                   },
                                   recognitionModel: recognitionModel).ConfigureAwait(true);

                return detectedFaces;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}