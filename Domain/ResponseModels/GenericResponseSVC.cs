using Service.Models;

namespace Service.ReturnModels
{
    public class GenericReturnDM : BaseReturnDM
    {
        public GenericSM? GenericReturn { get; set; }

        public List<GenericSM>? GenericListReturn { get; set; } = new List<GenericSM>();
    }
}
