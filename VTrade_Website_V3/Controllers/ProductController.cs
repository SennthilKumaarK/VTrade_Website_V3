using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using VTrade_Website_V3.Models;
using DAL.Repository;
using DAL.Models;
using VTrade_Website_V3.Attributes;

namespace VTrade_Website_V3.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [EncryptedActionParameter]
        public ActionResult Index(int? CategoryID, int? BrandID)
        {
            GetCategoryItems(CategoryID);
            GetBrandItems(BrandID);
            GetSortItems();
            return View();
        }

        private void GetCategoryItems(int? CategoryID)
        {

            Methods Repobj = new Methods();
            var CategoryItemList = new List<SelectListItem>();

            try
            {
                _getCategoryItems _getCategoryItemsObj = new _getCategoryItems();
                _getCategoryItemsObj = Repobj.getCategoryItems();

                if (_getCategoryItemsObj.ResponseStatus == true)
                {
                    if ((CategoryID != null) && (CategoryID > 0))
                    {
                        CategoryItemList.Add(new SelectListItem
                        {
                            Value = "0",
                            Text = "Select Category"
                        });

                        foreach (var element in _getCategoryItemsObj.lstCategoryItem)
                        {
                            if (element.ID == CategoryID)
                            {
                                CategoryItemList.Add(new SelectListItem
                                {
                                    Selected = true,
                                    Value = element.ID.ToString(),
                                    Text = element.CategoryName
                                });
                            }
                            else
                            {
                                CategoryItemList.Add(new SelectListItem
                                {
                                    Value = element.ID.ToString(),
                                    Text = element.CategoryName
                                });
                            }
                        }
                    }

                    else
                    {
                        CategoryItemList.Add(new SelectListItem
                        {
                            Selected = true,
                            Value = "0",
                            Text = "Select Category"
                        });

                        foreach (var element in _getCategoryItemsObj.lstCategoryItem)
                        {
                            CategoryItemList.Add(new SelectListItem
                            {
                                Value = element.ID.ToString(),
                                Text = element.CategoryName
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            ViewData["CategoryListItems"] = CategoryItemList;
        }

        private void GetBrandItems(int? BrandID)
        {

            Methods Repobj = new Methods();
            var BrandItemList = new List<SelectListItem>();

            try
            {
                _getBrandItems _getBrandItemsObj = new _getBrandItems();
                _getBrandItemsObj = Repobj.getBrandItems();

                if (_getBrandItemsObj.ResponseStatus == true)
                {
                    if ((BrandID != null) && (BrandID > 0))
                    {
                        BrandItemList.Add(new SelectListItem
                        {
                            Value = "0",
                            Text = "Select Brand"
                        });

                        foreach (var element in _getBrandItemsObj.lstBrandItem)
                        {
                            if (element.ID == BrandID)
                            {
                                BrandItemList.Add(new SelectListItem
                                {
                                    Selected = true,
                                    Value = element.ID.ToString(),
                                    Text = element.BrandName
                                });
                            }
                            else
                            {
                                BrandItemList.Add(new SelectListItem
                                {
                                    Value = element.ID.ToString(),
                                    Text = element.BrandName
                                });
                            }
                        }
                    }

                    else
                    {
                        BrandItemList.Add(new SelectListItem
                        {
                            Selected = true,
                            Value = "0",
                            Text = "Select Brand"
                        });

                        foreach (var element in _getBrandItemsObj.lstBrandItem)
                        {
                            BrandItemList.Add(new SelectListItem
                            {
                                Value = element.ID.ToString(),
                                Text = element.BrandName
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            ViewData["BrandListItems"] = BrandItemList;
        }

        private void GetSortItems()
        {

            var SortItemList = new List<SelectListItem>();

            SortItemList.Add(new SelectListItem
            {
                Selected = true,
                Value = "0",
                Text = "Category"
            });

            SortItemList.Add(new SelectListItem
            {
                Value = "1",
                Text = "Brand"
            });

            SortItemList.Add(new SelectListItem
            {
                Value = "2",
                Text = "New Items"
            });

            ViewData["SortListItems"] = SortItemList;
        }

        [EncryptedActionParameter]
        public ActionResult ProductDetail(int ProductID)
        {
            ViewData["ProductID"] = ProductID;
            return View();
        }

        [HttpGet]
        public PartialViewResult GetProductListItems(ProductFilter _ProductFilter)
        {
            ProductListResponseData res = new ProductListResponseData();
            Methods Repobj = new Methods();
            List<ProductListInfo> lstObj = new List<ProductListInfo>();

            _getProductListItems _getProductListItemsObj = new _getProductListItems();
            _getProductListItemsObj = Repobj.getProductListItems(_ProductFilter, 6);

            if (_getProductListItemsObj.ResponseStatus == true)
            {
                if (_getProductListItemsObj.lstProductItem != null)
                {
                    lstObj = _getProductListItemsObj.lstProductItem;
                }
            }

            res.lstProductItem = lstObj;
            res.PageNO = _getProductListItemsObj.PageNO;
            res.NumSize = _getProductListItemsObj.numSize;

            if (lstObj.Count > 0)
            {
                int StartPg = _getProductListItemsObj.StartPg;
                int TotalPg = _getProductListItemsObj.TotalPg;
                int StartCount = (StartPg + 1);
                int EndCount = (StartPg + lstObj.Count);

                if (EndCount > StartCount)
                {
                    res.PageDesc = "Showing results of " + StartCount + " - " + EndCount + " out of " + TotalPg + " products";
                }
                else
                {
                    res.PageDesc = "Showing results of " + StartCount + " out of " + TotalPg + " products";
                }
            }
            else
            {
                res.PageDesc = "Showing results 0 products";
            }

            return PartialView("GetProductListItems", res);

        }

        [HttpGet]
        public JsonResult GetProductInfo(int ProductID)
        {
            ProductInfoResponseData res = new ProductInfoResponseData();
            try
            {
                string str_responseImageData = "";
                string str_responseProductInfo = "";

                Methods Repobj = new Methods();
                _getProductImageList _getProductImageListObj = new _getProductImageList();
                _getProductImageListObj = Repobj.getProductImageItems(ProductID);

                if (_getProductImageListObj.ResponseStatus == true)
                {
                    List<ProductImageList> lstObj = new List<ProductImageList>();
                    lstObj = _getProductImageListObj.lstProductImageList;

                    if (lstObj != null)
                    {
                        foreach (var ProdImgInfoItem in lstObj)
                        {
                            str_responseImageData += "<div class='swiper-slide'><img src='" + ProdImgInfoItem.ProductImgPath + "' alt=''></div>";
                        }
                    }
                }

                _getProductItems _getProductItemsObj = new _getProductItems();

                var ProdID_List = new int[1];
                ProdID_List[0] = ProductID;

                _getProductItemsObj = Repobj.getProductItemsbyID(ProdID_List);

                if (_getProductItemsObj.ResponseStatus == true)
                {
                    List<ProductListInfo> ProductInfoObj = new List<ProductListInfo>();
                    ProductInfoObj = _getProductItemsObj.lstProductItem;

                    if ((ProductInfoObj != null) && (ProductInfoObj.Count > 0))
                    {
                        res.ProductName = ProductInfoObj[0].ProductName;
                        res.ProductDesc = ProductInfoObj[0].ProductDesc;

                        str_responseProductInfo += "<h3>Product Information</h3>";
                        str_responseProductInfo += "<ul>";
                        str_responseProductInfo += "<li><strong>Product Name</strong>: " + ProductInfoObj[0].ProductName + "</li>";
                        str_responseProductInfo += "<li><strong>Brand Name</strong>: " + ProductInfoObj[0].BrandName + "</li>";
                        str_responseProductInfo += "<li><strong>Category</strong>: " + ProductInfoObj[0].CategoryName + "</li>";


                        _getProductInfo _getProductInfoObj = new _getProductInfo();
                        _getProductInfoObj = Repobj.getProductInfo(ProductID);

                        if (_getProductInfoObj.ResponseStatus == true)
                        {
                            List<ProductInfo> lstObj = new List<ProductInfo>();
                            lstObj = _getProductInfoObj.lstProductInfo;

                            if (lstObj != null)
                            {
                                foreach (var ProdInfoItem in lstObj)
                                {
                                    str_responseProductInfo += "<li><strong>" + ProdInfoItem.KeyName + "</strong>: " + ProdInfoItem.KeyValue + "</li>";
                                }
                            }
                        }

                        str_responseProductInfo += "</ul>";
                    }

                }

                res.ProductImageList = str_responseImageData;
                res.ProductInformation = str_responseProductInfo;
                res.ResponseSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}