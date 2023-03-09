using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Methods
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

        public _getProductItems getProductItemsbyID(int[] ProdID_List)
        {

            _getProductItems _getProductItemsObj = new _getProductItems();
            try
            {
                List<ProductListInfo> lstProdItemObj = new List<ProductListInfo>();

                var result = (from prod in entityobj.ProductItems
                              join Brands in entityobj.BrandItems on prod.BrandID equals Brands.ID
                              join catitems in entityobj.CategoryItems on prod.CatID equals catitems.ID
                              where (prod.Status != "D") && (Brands.Status != "D") && (catitems.Status != "D")
                              //&& (prod.ID == 39 || prod.ID == 46 || prod.ID == 12 || prod.ID == 15 || prod.ID == 3 || prod.ID == 19)
                              select new
                              {
                                  prod.ID,
                                  prod.ProductName,
                                  prod.ProductImgPath,
                                  prod.ProductDesc,
                                  Brands.BrandName,
                                  catitems.CategoryName
                              }).Where(e => ProdID_List.Contains(e.ID)).ToList();

                foreach (var item in result)
                {
                    ProductListInfo ProductItemObj = new ProductListInfo();

                    ProductItemObj.ID = item.ID;
                    ProductItemObj.ProductName = item.ProductName;
                    ProductItemObj.ProductDesc = item.ProductDesc;
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

        public _getProductInfo getProductInfo(int ProductID)
        {
            _getProductInfo _getProductInfoObj = new _getProductInfo();

            try
            {
                List<ProductInfo> lstProductInfoObj = new List<ProductInfo>();

                var result = (from prod in entityobj.ProductInfoes
                              where (prod.Status != "D") && (prod.ProductID == ProductID)
                              select new
                              {
                                  ID = prod.ID,
                                  KeyName = prod.KeyName,
                                  KeyValue = prod.KeyValue
                              }).OrderBy(r => r.ID).ToList();

                foreach (var item in result)
                {
                    ProductInfo ProductInfoObj = new ProductInfo();

                    ProductInfoObj.KeyName = item.KeyName;
                    ProductInfoObj.KeyValue = item.KeyValue;

                    lstProductInfoObj.Add(ProductInfoObj);
                }

                _getProductInfoObj.ResponseStatus = true;
                _getProductInfoObj.lstProductInfo = lstProductInfoObj;
            }
            catch (Exception ex)
            {
                _getProductInfoObj.ResponseStatus = false;
                _getProductInfoObj.ErrorMessage = ex.Message.ToString();
            }

            return _getProductInfoObj;
        }

        public _getProductImageList getProductImageItems(int ProductID)
        {

            _getProductImageList _getProductImageListObj = new _getProductImageList();
            try
            {
                List<ProductImageList> lstProductImageListObj = new List<ProductImageList>();

                var result = (from prod in entityobj.ProductImageLists
                              where (prod.Status != "D") && (prod.ProductID == ProductID)
                              select new
                              {
                                  ID = prod.ID,
                                  ProductID = prod.ProductID,
                                  ProductImgPath = prod.ProductImgPath
                              }).OrderBy(r => r.ID).ToList();

                foreach (var item in result)
                {
                    ProductImageList ProductImageListObj = new ProductImageList();

                    ProductImageListObj.ID = item.ID;
                    ProductImageListObj.ProductID = item.ProductID;
                    ProductImageListObj.ProductImgPath = item.ProductImgPath;

                    lstProductImageListObj.Add(ProductImageListObj);
                }

                _getProductImageListObj.ResponseStatus = true;
                _getProductImageListObj.lstProductImageList = lstProductImageListObj;
            }
            catch (Exception ex)
            {
                _getProductImageListObj.ResponseStatus = false;
                _getProductImageListObj.ErrorMessage = ex.Message.ToString();
            }


            return _getProductImageListObj;
        }

        public _getBrandItems getBrandItems()
        {

            _getBrandItems _getBrandItemsObj = new _getBrandItems();
            try
            {
                List<BrandItem> lstBrandItemObj = new List<BrandItem>();

                var result = (from Brands in entityobj.BrandItems
                              where (Brands.Status != "D")
                              select new
                              {
                                  BrandID = Brands.ID,
                                  BrandName = Brands.BrandName,
                                  BrandDesc = Brands.BrandDesc,
                                  BrandImgPath = Brands.BrandImgPath
                              }).ToList();

                foreach (var item in result)
                {
                    BrandItem BrandItemObj = new BrandItem();

                    BrandItemObj.ID = item.BrandID;
                    BrandItemObj.BrandName = item.BrandName;
                    BrandItemObj.BrandDesc = item.BrandDesc;
                    BrandItemObj.BrandImgPath = item.BrandImgPath;

                    lstBrandItemObj.Add(BrandItemObj);
                }

                _getBrandItemsObj.ResponseStatus = true;
                _getBrandItemsObj.lstBrandItem = lstBrandItemObj;
            }
            catch (Exception ex)
            {
                _getBrandItemsObj.ResponseStatus = false;
                _getBrandItemsObj.ErrorMessage = ex.Message.ToString();
            }


            return _getBrandItemsObj;
        }

        public _getBrandListItems getBrandListItems(int PageNO, int pgSize)
        {

            _getBrandListItems _getBrandItemsObj = new _getBrandListItems();
            try
            {
                List<BrandItem> lstBrandItemObj = new List<BrandItem>();

                var result = (from Brands in entityobj.BrandItems
                              where (Brands.Status != "D")
                              select new
                              {
                                  BrandID = Brands.ID,
                                  BrandName = Brands.BrandName,
                                  BrandImgPath = Brands.BrandImgPath
                              }).ToList();


                if (PageNO <= 0)
                {
                    PageNO = 1;
                }

                int start = (int)(PageNO - 1) * pgSize;
                int totalPage = result.Count();
                float totalNumsize = (totalPage / (float)pgSize);
                int numSize = (int)Math.Ceiling(totalNumsize);
                var BrandListFilter = result.Skip(start).Take(pgSize);

                foreach (var item in BrandListFilter)
                {
                    BrandItem BrandItemObj = new BrandItem();

                    BrandItemObj.ID = item.BrandID;
                    BrandItemObj.BrandName = item.BrandName;
                    BrandItemObj.BrandImgPath = item.BrandImgPath;

                    lstBrandItemObj.Add(BrandItemObj);
                }

                _getBrandItemsObj.ResponseStatus = true;
                _getBrandItemsObj.lstBrandItem = lstBrandItemObj;
                _getBrandItemsObj.PageNO = PageNO;
                _getBrandItemsObj.numSize = numSize;
                _getBrandItemsObj.StartPg = start;
                _getBrandItemsObj.TotalPg = totalPage;
            }
            catch (Exception ex)
            {
                _getBrandItemsObj.ResponseStatus = false;
                _getBrandItemsObj.ErrorMessage = ex.Message.ToString();
            }


            return _getBrandItemsObj;
        }

        public _getProductListItems getProductListItems(ProductFilter _ProductFilter, int pgSize)
        {

            _getProductListItems _getProductListItemsObj = new _getProductListItems();
            try
            {
                List<ProductListInfo> lstProdItemObj = new List<ProductListInfo>();

                var result = (from prod in entityobj.ProductItems
                              join Brands in entityobj.BrandItems on prod.BrandID equals Brands.ID
                              join catitems in entityobj.CategoryItems on prod.CatID equals catitems.ID
                              where (prod.Status != "D") && (Brands.Status != "D")
                              select new
                              {
                                  ProductID = prod.ID,
                                  ProductName = prod.ProductName,
                                  ProductImgPath = prod.ProductImgPath,
                                  BrandName = Brands.BrandName,
                                  CategoryName = catitems.CategoryName,
                                  CategoryID = catitems.ID,
                                  BrandID = Brands.ID,
                                  ProductOrder = prod.ProductOrder
                              }).ToList();

                if (_ProductFilter.CategoryID != "0")
                {
                    result = result.Where(r => r.CategoryID.ToString() == _ProductFilter.CategoryID).ToList();
                }

                if (_ProductFilter.BrandID != "0")
                {
                    result = result.Where(r => r.BrandID.ToString() == _ProductFilter.BrandID).ToList();
                }

                if (_ProductFilter.SortID == "0")
                {
                    result = result.OrderBy(r => r.CategoryName).ToList();
                }

                else if (_ProductFilter.SortID == "1")
                {
                    result = result.OrderBy(r => r.BrandName).ToList();
                }

                else if (_ProductFilter.SortID == "2")
                {
                    result = result.OrderBy(r => r.ProductOrder).ToList();
                }

                if (_ProductFilter.PageNO <= 0)
                {
                    _ProductFilter.PageNO = 1;
                }

                int start = (int)(_ProductFilter.PageNO - 1) * pgSize;
                int totalPage = result.Count();
                float totalNumsize = (totalPage / (float)pgSize);
                int numSize = (int)Math.Ceiling(totalNumsize);
                var ProductListFilter = result.Skip(start).Take(pgSize);

                foreach (var item in ProductListFilter)
                {
                    ProductListInfo ProductItemObj = new ProductListInfo();

                    ProductItemObj.ID = item.ProductID;
                    ProductItemObj.ProductName = item.ProductName;
                    ProductItemObj.ProductImgPath = item.ProductImgPath;
                    ProductItemObj.BrandName = item.BrandName;
                    ProductItemObj.CategoryName = item.CategoryName;

                    lstProdItemObj.Add(ProductItemObj);
                }

                _getProductListItemsObj.ResponseStatus = true;
                _getProductListItemsObj.lstProductItem = lstProdItemObj;
                _getProductListItemsObj.PageNO = _ProductFilter.PageNO;
                _getProductListItemsObj.numSize = numSize;
                _getProductListItemsObj.StartPg = start;
                _getProductListItemsObj.TotalPg = totalPage;
            }
            catch (Exception ex)
            {
                _getProductListItemsObj.ResponseStatus = false;
                _getProductListItemsObj.ErrorMessage = ex.Message.ToString();
            }


            return _getProductListItemsObj;
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


        public _getUniqueYearVisitorAnalytics getUniqueVisitorAnalyticsYear()
        {
            _getUniqueYearVisitorAnalytics _getUniqueYearVisitorAnalyticsObj = new _getUniqueYearVisitorAnalytics();

            try
            {
                List<string> lstYear = new List<string>();

                var result = (from vstAnalytic in entityobj.VisitorAnalytics
                              select new
                              {
                                  vstAnalytic.ANTRYEAR
                              }).GroupBy(r => r.ANTRYEAR).Select(g => g.FirstOrDefault()).ToList();

                foreach (var element in result)
                {
                    lstYear.Add(element.ANTRYEAR);
                }

                _getUniqueYearVisitorAnalyticsObj.ResponseStatus = true;
                _getUniqueYearVisitorAnalyticsObj.lstYear = lstYear;

            }
            catch (Exception ex)
            {
                _getUniqueYearVisitorAnalyticsObj.ResponseStatus = false;
                _getUniqueYearVisitorAnalyticsObj.ErrorMessage = ex.Message.ToString();
            }

            return _getUniqueYearVisitorAnalyticsObj;
        }


        public _getDashboardCount getDashboardCount()
        {
            _getDashboardCount _getDashboardCountObj = new _getDashboardCount();

            try
            {
                var VisitorsCount = (from _VisitorAnalytic in entityobj.VisitorAnalytics
                                     select new
                                     {
                                         _VisitorAnalytic.ID
                                     }
                                   ).ToList();
                _getDashboardCountObj.TotalVisitorCount = VisitorsCount.Count().ToString();

                string sCurrentDate = DateTime.Now.ToString("yyyyMMdd");
                var VisitorsCountToday = (from _VisitorAnalytic in entityobj.VisitorAnalytics
                                          where (_VisitorAnalytic.ANTRDATE == sCurrentDate)
                                          select new
                                          {
                                              _VisitorAnalytic.ID
                                          }
                                   ).ToList();
                _getDashboardCountObj.TotalVisitorToday = VisitorsCountToday.Count().ToString();


                var ProductsCount = (from _ProductItem in entityobj.ProductItems
                                     where (_ProductItem.Status != "D")
                                     select new
                                     {
                                         _ProductItem.ID
                                     }
                                   ).ToList();
                _getDashboardCountObj.TotalProducts = ProductsCount.Count().ToString();


                var SubscribeCount = (from _SubscribeDetail in entityobj.SubscribeDetails
                                      select new
                                      {
                                          _SubscribeDetail.ID
                                      }
                                   ).ToList();
                _getDashboardCountObj.TotalSubscribe = SubscribeCount.Count().ToString();


                var ContactCount = (from _ContactDetail in entityobj.ContactDetails
                                    select new
                                    {
                                        _ContactDetail.ID
                                    }
                                    ).ToList();
                _getDashboardCountObj.TotalContact = ContactCount.Count().ToString();

                _getDashboardCountObj.ResponseStatus = true;

            }
            catch (Exception ex)
            {
                _getDashboardCountObj.ResponseStatus = false;
                _getDashboardCountObj.ErrorMessage = ex.Message.ToString();
            }

            return _getDashboardCountObj;
        }

        public _getDashboardChartData getDashboardChartData(string YearVal)
        {
            _getDashboardChartData _getDashboardChartDataObj = new _getDashboardChartData();

            try
            {
                List<string> countval = new List<string>();

                for (int iMonth = 1; iMonth <= 12; iMonth++)
                {
                    string sMonth = iMonth.ToString("00");
                    var VisitorsCount = (from _VisitorAnalytic in entityobj.VisitorAnalytics
                                         where (_VisitorAnalytic.ANTRYEAR == YearVal) & (_VisitorAnalytic.ANTRMONTH == sMonth)
                                         select new
                                         {
                                             _VisitorAnalytic.ID
                                         }
                                   ).ToList();

                    countval.Add(VisitorsCount.Count().ToString());
                }

                _getDashboardChartDataObj.lstcountval = countval;
                _getDashboardChartDataObj.ResponseStatus = true;

            }
            catch (Exception ex)
            {
                _getDashboardChartDataObj.ResponseStatus = false;
                _getDashboardChartDataObj.ErrorMessage = ex.Message.ToString();
            }

            return _getDashboardChartDataObj;
        }


        public _chkLoginStatus getLoginStatus(string User_Name, string User_Password)
        {

            _chkLoginStatus _chkLoginStatusObj = new _chkLoginStatus();
            try
            {
                var result = (from userDet in entityobj.UserDetails
                              where (userDet.USERNAME.ToUpper() == User_Name.ToUpper()) && (userDet.USERPASSWORD == User_Password)
                              select new
                              {
                                  userDet.ID,
                                  userDet.USERNAME,
                                  userDet.USERPASSWORD,
                              }).FirstOrDefault();

                if (result != null)
                {
                    _chkLoginStatusObj.ResponseStatus = true;
                    _chkLoginStatusObj.IsUserExist = true;
                    _chkLoginStatusObj.UserName = result.USERNAME;
                }
                else
                {
                    _chkLoginStatusObj.ResponseStatus = true;
                    _chkLoginStatusObj.IsUserExist = false;
                    _chkLoginStatusObj.UserMessage = "Invalid Username or Password";
                    _chkLoginStatusObj.UserName = "";
                }


            }
            catch (Exception ex)
            {
                _chkLoginStatusObj.ResponseStatus = false;
                _chkLoginStatusObj.ErrorMessage = ex.Message.ToString();
            }


            return _chkLoginStatusObj;
        }


    }
}
