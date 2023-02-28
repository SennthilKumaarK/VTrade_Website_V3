using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Repository
    {
        VtradellcdbEntities entityobj = new VtradellcdbEntities();


        public _getCategoryItems getCategoryItems()
        {
            _getCategoryItems _getCategoryItemsObj = new _getCategoryItems();

            try
            {
                List<CategoryItem> lstObj = new List<CategoryItem>();

                var result = (from category in entityobj.CategoryItems
                              where (category.Status != "D")
                              select new
                              {
                                  category.ID,
                                  category.CategoryName,
                                  category.CategoryDesc,
                                  category.CategoryImgPath,
                                  category.Status
                              }).ToList();

                foreach (var item in result)
                {
                    CategoryItem CategoryItemobj = new CategoryItem();

                    CategoryItemobj.ID = item.ID;
                    CategoryItemobj.CategoryName = item.CategoryName;
                    CategoryItemobj.CategoryDesc = item.CategoryDesc;
                    CategoryItemobj.CategoryImgPath = item.CategoryImgPath;
                    CategoryItemobj.Status = item.Status;

                    lstObj.Add(CategoryItemobj);
                }

                _getCategoryItemsObj.ResponseStatus = true;
                _getCategoryItemsObj.lstCategoryItem = lstObj;
            }
            catch (Exception ex)
            {
                _getCategoryItemsObj.ResponseStatus = false;
                _getCategoryItemsObj.ErrorMessage = ex.Message.ToString();
            }

            return _getCategoryItemsObj;
        }


        public _getProductItems getProductItems()
        {

            _getProductItems _getProductItemsObj = new _getProductItems();
            try
            {
                List<ProductListInfo> lstProdItemObj = new List<ProductListInfo>();

                var result = (from prod in entityobj.ProductItems
                              join Brands in entityobj.BrandItems on prod.BrandID equals Brands.ID
                              join catitems in entityobj.CategoryItems on prod.CatID equals catitems.ID
                              where (prod.Status != "D") && (Brands.Status != "D") && (catitems.Status != "D") && (prod.ID == 39 || prod.ID == 46 || prod.ID == 12 || prod.ID == 15 || prod.ID == 3 || prod.ID == 19)
                              select new
                              {
                                  prod.ID,
                                  prod.ProductName,
                                  prod.ProductImgPath,
                                  Brands.BrandName,
                                  catitems.CategoryName
                              }).ToList();

                foreach (var item in result)
                {
                    ProductListInfo ProductItemObj = new ProductListInfo();

                    ProductItemObj.ID = item.ID;
                    ProductItemObj.ProductName = item.ProductName;
                    ProductItemObj.ProductImgPath = item.ProductImgPath;
                    ProductItemObj.BrandName = item.BrandName;
                    ProductItemObj.CategoryName = item.CategoryName;

                    lstProdItemObj.Add(ProductItemObj);
                }

                _getProductItemsObj.ResponseStatus = true;
                _getProductItemsObj.lstProductItem = lstProdItemObj;
            }
            catch (Exception ex)
            {
                _getProductItemsObj.ResponseStatus = false;
                _getProductItemsObj.ErrorMessage = ex.Message.ToString();
            }


            return _getProductItemsObj;
        }

        public _getUpdateStatus insertSubscribeEmail(string subcribeemailaddress)
        {
            _getUpdateStatus _setSubscribeEmailObj = new _getUpdateStatus();
            try
            {
                string CREDATE = DateTime.Now.ToString("yyyyMMdd");
                string CRETIME = DateTime.Now.ToString("HHmmssfff");

                SubscribeDetail subscribe = new SubscribeDetail();
                subscribe.SubscribeEmail = subcribeemailaddress;
                subscribe.SubscribeDate = CREDATE;
                subscribe.SubscribeTime = CRETIME;
                entityobj.SubscribeDetails.Add(subscribe);
                entityobj.SaveChanges();

                _setSubscribeEmailObj.ResponseStatus = true;
            }
            catch (Exception ex)
            {
                _setSubscribeEmailObj.ResponseStatus = false;
                _setSubscribeEmailObj.ErrorMessage = ex.Message.ToString();
            }

            return _setSubscribeEmailObj;
        }

        public _getUpdateStatus insertContactDetails(ContactDetail _ContactDetail)
        {
            _getUpdateStatus _getUpdateStatusObj = new _getUpdateStatus();
            try
            {
                string CREDATE = DateTime.Now.ToString("yyyyMMdd");
                string CRETIME = DateTime.Now.ToString("HHmmssfff");

                ContactDetail objContactDetail = new ContactDetail();
                objContactDetail.ContactEmail = _ContactDetail.ContactEmail;
                objContactDetail.ContactName = _ContactDetail.ContactName;
                objContactDetail.ContactMessage = _ContactDetail.ContactMessage;
                objContactDetail.ContactNumber = _ContactDetail.ContactNumber;
                objContactDetail.ContactDate = CREDATE;
                objContactDetail.ContactTime = CRETIME;
                entityobj.ContactDetails.Add(objContactDetail);
                entityobj.SaveChanges();

                _getUpdateStatusObj.ResponseStatus = true;
            }
            catch (Exception ex)
            {
                _getUpdateStatusObj.ResponseStatus = false;
                _getUpdateStatusObj.ErrorMessage = ex.Message.ToString();
            }

            return _getUpdateStatusObj;
        }

        public _getCountryCodes getCountryCodeList()
        {
            _getCountryCodes _getCountryCodesObj = new _getCountryCodes();

            try
            {
                List<CountryCodes> lstCountryCodes = new List<CountryCodes>();

                XMLReader readXML = new XMLReader();
                var xmlData = readXML.ReturnListOfCountryCodes();

                foreach (var element in xmlData)
                {
                    CountryCodes CountryCodesObj = new CountryCodes();
                    CountryCodesObj.CountryCode = element.CountryCode;
                    CountryCodesObj.CountryName = element.CountryName;
                    lstCountryCodes.Add(CountryCodesObj);
                }

                _getCountryCodesObj.ResponseStatus = true;
                _getCountryCodesObj.lstCountryCodes = lstCountryCodes;

            }
            catch (Exception ex)
            {
                _getCountryCodesObj.ResponseStatus = false;
                _getCountryCodesObj.ErrorMessage = ex.Message.ToString();
            }

            return _getCountryCodesObj;
        }
    }
}
