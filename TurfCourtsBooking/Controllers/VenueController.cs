using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurfCourtsBooking.DatabaseConnection;
using TurfCourtsBooking.Models;

namespace VenueController
{
    public class VenueController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region Venue View
        public ActionResult VenueView()
        {
            var venue = DatabaseConnection.VenueDataView();
            return View(venue);
        }
        #endregion

        #region Venue Add View
        public ActionResult VenueAddView()
        {
            return View();
        }
        #endregion

        #region Venue Add
        [HttpPost]
        public ActionResult VenueAdd(VenueModel venue)
        {
            bool result = DatabaseConnection.VenueDataAdd(venue);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = "Venue added successfully.";
                return RedirectToAction("VenueView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Venue registration failed!";
                return RedirectToAction("VenueAdd");
            }
        }
        #endregion

        #region Venue Edit
        [HttpGet]
        public ActionResult VenueEditView(int id)
        {
            var venue = DatabaseConnection.VenueDataEdit(id);
            return View(venue);
        }
        #endregion

        #region Venue Update
        [HttpPost]
        public ActionResult VenueEdit(VenueModel venue)
        {
            bool result = DatabaseConnection.VenueDataUpdate(venue);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = "Venue updated successfully.";
                return RedirectToAction("VenueView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Venue updation failed!";
                return RedirectToAction("VenueEditView");
            }
        }
        #endregion

        #region Venue Delete
        public ActionResult VenueDelete(int id)
        {
            var result = DatabaseConnection.VenueDataDelete(id);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = "Venue Deleted Successfully.";
                return RedirectToAction("VenueView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Venue Deletion Failed!";
                return RedirectToAction("VenueView");
            }
        }
        #endregion
    }
}