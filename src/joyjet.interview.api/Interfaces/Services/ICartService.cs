using joyjet_interview_test.ApiModels;
using joyjet_interview_test.Models;

namespace joyjet_interview_test.Interfaces.Services
{
    public interface ICartService
    {
        IEnumerable<PostCartResult> CalculateCart(PostCartInput param);        
    }
}