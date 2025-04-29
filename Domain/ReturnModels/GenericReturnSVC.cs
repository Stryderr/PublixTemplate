using Service.Models;

namespace Service.ReturnModels
{
    public class GenericReturnDM : BaseReturnDM
    {
        public GenericDM? GenericReturn { get; set; }

        public List<GenericDM>? GenericListReturn { get; set; } = new List<GenericDM>();
    }
}
