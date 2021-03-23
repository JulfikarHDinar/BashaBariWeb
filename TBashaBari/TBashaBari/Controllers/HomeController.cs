using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TBashaBari.Models;
using TBashaBari.Controllers;
using System.Dynamic;

namespace TBashaBari.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<OwnerNotice> _ownernoticelist = new List<OwnerNotice>();
        List<TenantRequest> _tenantrequestlist = new List<TenantRequest>();
        List<TenantConnectsOwner> _tenantconnectsownerlist = new List<TenantConnectsOwner>();
        List<BillInformation> _billinformationlist = new List<BillInformation>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            FetchOwnerNotice();
            FetchTenantRequest();
            FetchPendingTenants();
            FetchBillInfo();
            dynamic homeViewModel = new ExpandoObject();
            homeViewModel.Notice = _ownernoticelist;
            homeViewModel.Request = _tenantrequestlist;
            homeViewModel.Connect = _tenantconnectsownerlist;
            homeViewModel.Bill = _billinformationlist;
            return View(homeViewModel);
        }
        public IActionResult OwnerHome()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
        //:::::::::::::::::::::::::::::         custom methods        ::::::::::::::::::::::::::::::::://
        private void FetchOwnerNotice()
        {
            //User.Identity.Name returns current logged in user's email
            string queryString = "SELECT TOP 10 [NoticeId],[OwnerEmail],[NoticeText],[NoticeTime] " +
                                        "FROM [BashaBariWeb].[dbo].[OwnerNotice] " +
                                        "WHERE [OwnerEmail] = '" + User.Identity.Name + "' " +
                                        "ORDER BY [NoticeTime] DESC";
            //to clear the list initially
            if (_ownernoticelist.Count > 0)
            {
                _ownernoticelist.Clear();
            }

            //database operation
            DatabaseConnection obj = new DatabaseConnection();
            obj.DbConnect();
            while (obj.ExeQuery(queryString).Read())
            {
                _ownernoticelist.Add(new OwnerNotice
                {
                    NoticeId = int.Parse(obj.ExeQuery(queryString)["NoticeId"].ToString()),
                    OwnerEmail = obj.ExeQuery(queryString)["OwnerEmail"].ToString(),
                    NoticeText = obj.ExeQuery(queryString)["NoticeText"].ToString(),
                    NoticeTime = obj.ExeQuery(queryString)["NoticeTime"].ToString(),
                });
            }
            obj.CloseDbConnect();
        }

        private void FetchTenantRequest()
        {
            //User.Identity.Name returns current logged in user's email
            string queryString = "SELECT TOP 10 T.[RequestId], C.[TenantEmail], T.[RequestText], T.[RequestTime], T.[CommentOnRequestText], T.[CommentOnRequestTime] " +
                                        "FROM [BashaBariWeb].[dbo].[TenantConnectsOwner] C JOIN [BashaBariWeb].[dbo].[TenantRequest] T " +
                                        "ON C.[TenantEmail] = T.[TenantEmail] " +
                                        "WHERE C.[IsConfirmed] ='Yes' AND C.[OwnerEmail] = '" + User.Identity.Name + "' " +
                                        "ORDER BY T.[RequestTime] DESC";
            //to clear the list initially
            if (_tenantrequestlist.Count > 0)
            {
                _tenantrequestlist.Clear();
            }

            //database operation
            DatabaseConnection obj = new DatabaseConnection();
            obj.DbConnect();
            while (obj.ExeQuery(queryString).Read())
            {
                _tenantrequestlist.Add(new TenantRequest
                {
                    RequestId = int.Parse(obj.ExeQuery(queryString)["RequestId"].ToString()),
                    TenantEmail = obj.ExeQuery(queryString)["TenantEmail"].ToString(),
                    RequestText = obj.ExeQuery(queryString)["RequestText"].ToString(),
                    RequestTime = obj.ExeQuery(queryString)["RequestTime"].ToString(),
                    CommentOnRequestText = obj.ExeQuery(queryString)["CommentOnRequestText"].ToString(),
                    CommentOnRequestTime = obj.ExeQuery(queryString)["CommentOnRequestTime"].ToString(),
                });
            }
            obj.CloseDbConnect();
        }

        private void FetchPendingTenants()
        {
            //User.Identity.Name returns current logged in user's email
            string queryString = "SELECT TOP 10 [ConnectionId],[TenantEmail],[OwnerEmail],[IsConfirmed] " +
                                        "FROM [BashaBariWeb].[dbo].[TenantConnectsOwner] " +
                                        "WHERE [OwnerEmail] = '" + User.Identity.Name + "' AND [IsConfirmed] = 'No'" +
                                        "ORDER BY [ConnectionId] DESC";
            //to clear the list initially
            if (_tenantconnectsownerlist.Count > 0)
            {
                _tenantconnectsownerlist.Clear();
            }

            //database operation
            DatabaseConnection obj = new DatabaseConnection();
            obj.DbConnect();
            while (obj.ExeQuery(queryString).Read())
            {
                _tenantconnectsownerlist.Add(new TenantConnectsOwner
                {
                    ConnectionId = int.Parse(obj.ExeQuery(queryString)["ConnectionId"].ToString()),
                    TenantEmail = obj.ExeQuery(queryString)["TenantEmail"].ToString(),
                    OwnerEmail = obj.ExeQuery(queryString)["OwnerEmail"].ToString(),
                    IsConfirmed = obj.ExeQuery(queryString)["IsConfirmed"].ToString(),
                });
            }
            obj.CloseDbConnect();
        }

        private void FetchBillInfo()
        {
            //User.Identity.Name returns current logged in user's email
            string queryString = "SELECT TOP 1000 [BillId],[OwnerEmail],[TenantEmail],[BillTime],[WaterAmount],[WaterPaid],[WaterVerified],[ElectricAmount]," +
                                        "[ElectricPaid],[ElectricVerified],[RentAmount],[RentPaid],[RentVerified],[GasAmount],[GasPaid],[GasVerified] " +
                                        "FROM [BashaBariWeb].[dbo].[BillInformation] " +
                                        "WHERE [OwnerEmail] = '" + User.Identity.Name + "' " +
                                        "ORDER BY [BillTime] DESC";
            //to clear the list initially
            if (_billinformationlist.Count > 0)
            {
                _billinformationlist.Clear();
            }

            //database operation
            DatabaseConnection obj = new DatabaseConnection();
            obj.DbConnect();
            while (obj.ExeQuery(queryString).Read())
            {
                _billinformationlist.Add(new BillInformation
                {
                    BillId = int.Parse(obj.ExeQuery(queryString)["BillId"].ToString()),
                    OwnerEmail = obj.ExeQuery(queryString)["OwnerEmail"].ToString(),
                    TenantEmail = obj.ExeQuery(queryString)["TenantEmail"].ToString(),
                    BillTime = obj.ExeQuery(queryString)["BillTime"].ToString(),

                    WaterAmount = obj.ExeQuery(queryString)["WaterAmount"].ToString(),
                    WaterPaid = obj.ExeQuery(queryString)["WaterPaid"].ToString(),
                    WaterVerified = obj.ExeQuery(queryString)["WaterVerified"].ToString(),

                    ElectricAmount = obj.ExeQuery(queryString)["ElectricAmount"].ToString(),
                    ElectricPaid = obj.ExeQuery(queryString)["ElectricPaid"].ToString(),
                    ElectricVerified = obj.ExeQuery(queryString)["ElectricVerified"].ToString(),

                    RentAmount = obj.ExeQuery(queryString)["RentAmount"].ToString(),
                    RentPaid = obj.ExeQuery(queryString)["RentPaid"].ToString(),
                    RentVerified = obj.ExeQuery(queryString)["RentVerified"].ToString(),

                    GasAmount = obj.ExeQuery(queryString)["GasAmount"].ToString(),
                    GasPaid = obj.ExeQuery(queryString)["GasPaid"].ToString(),
                    GasVerified = obj.ExeQuery(queryString)["GasVerified"].ToString(),
                });
            }
            obj.CloseDbConnect();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
