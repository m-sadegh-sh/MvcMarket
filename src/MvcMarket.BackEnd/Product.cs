namespace MvcMarket.BackEnd
{
    using System.ComponentModel;

    public partial class Product : IDataErrorInfo
    {
        public string this[string propName]
        {
            get
            {
                if ((propName == "Name") && string.IsNullOrEmpty(Name))
                    return "Please enter a product name";
                if ((propName == "Description") && string.IsNullOrEmpty(Description))
                    return "Please enter a description";
                if ((propName == "Price") && (Price < 0))
                    return "Price must not be negative";
                return null;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}