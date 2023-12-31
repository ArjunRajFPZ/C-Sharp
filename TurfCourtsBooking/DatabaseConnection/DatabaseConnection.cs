﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TurfCourtsBooking.Models;

namespace TurfCourtsBooking.DatabaseConnection
{
    public class DatabaseConnection
    {
        string connString = ConfigurationManager.ConnectionStrings["databaseconnection"].ToString();

        #region Login
        public RegistrationModel LoginCheck(string email, string password)
        {
            RegistrationModel check = new RegistrationModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Login";
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    check.Email = reader["Email"].ToString();
                    check.Usertype = reader["Usertype"].ToString();
                }
                connection.Close();
            }
            return check;
        }
        #endregion

        #region Datewise Slot Details
        public List<TurfBookingModel> CheckSlot(TurfBookingModel datecheck)
        {
            List<TurfBookingModel> checkList = new List<TurfBookingModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SlotCheck";
                command.Parameters.AddWithValue("@Date", datecheck.Date);
                command.Parameters.AddWithValue("@Turftype", datecheck.Turftype);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    checkList.Add(new TurfBookingModel
                    {
                        Slots = dataRow["SlotId"].ToString(),
                    });
                }
            }
            return checkList;
        }
        #endregion

        #region Email Check
        public RegistrationModel EmailCheck(RegistrationModel user)
        {
            RegistrationModel check = new RegistrationModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "EmailCheck";
                command.Parameters.AddWithValue("@Email", user.Email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    check.Email = reader["Email"].ToString();
                }
                connection.Close();
            }
            return check;
        }
        #endregion

        #region Check Sports
        public List<SportModel> CheckSports(string Turftype)
        {
            List<SportModel> sportsList = new List<SportModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SportData";
                command.Parameters.AddWithValue("@Turftype", Turftype);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    sportsList.Add(new SportModel
                    {
                        Name = dataRow["Name"].ToString(),
                        Turftype = dataRow["Turftype"].ToString(),
                    });
                }
            }
            return sportsList;
        }
        #endregion

        #region User Data Retrieving
        public RegistrationModel UserDetails(string email)
        {
            RegistrationModel userDetails = new RegistrationModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "userDetails";
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userDetails.Name = reader["Name"].ToString();
                    userDetails.Dateofbirth = Convert.ToDateTime(reader["Dateofbirth"]).Date;
                    userDetails.Phone = reader["Phone"].ToString();
                    userDetails.Email = reader["Email"].ToString();
                }
                connection.Close();
            }
            return userDetails;
        }
        #endregion

        #region User Add
        public bool UserDataAdd(RegistrationModel user)
        {
            int returnValue = 0;
            Guid Id = Guid.NewGuid();
            string usertype = "User";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserAdd";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Dateofbirth", user.Dateofbirth);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Usertype", usertype);

                connection.Open();
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnValue > 0 ? true : false;
        }
        #endregion

        #region User View
        public List<RegistrationModel> UserDataView()
        {
            List<RegistrationModel> userList = new List<RegistrationModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    userList.Add(new RegistrationModel
                    {
                        Name = dataRow["Name"].ToString(),
                        Dateofbirth = Convert.ToDateTime(dataRow["Dateofbirth"]).Date,
                        Phone = dataRow["Phone"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Password = dataRow["Password"].ToString(),
                        Usertype = dataRow["Usertype"].ToString(),
                    });
                }
            }
            return userList;
        }
        #endregion

        #region Booking Add
        public bool TurfBookingAdd(TurfBookingModel booking)
        {
            int returnValue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                String str = "";
                for (int i = 0; i <= booking.SlotId.Count - 1; i++){
                    if (booking.SlotId.Count != 0){
                        if (str == ""){
                            str = booking.SlotId[i];
                        }else{
                            str += "," + booking.SlotId[i];
                        }
                    }
                }
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BookingAdd";
                command.Parameters.AddWithValue("@Username", booking.Username);
                command.Parameters.AddWithValue("@Phone", booking.Phone);
                command.Parameters.AddWithValue("@Email", booking.Email);
                command.Parameters.AddWithValue("@Date", booking.Date);
                command.Parameters.AddWithValue("@Sport", booking.Sport);
                command.Parameters.AddWithValue("@SlotId", str);
                command.Parameters.AddWithValue("@Turftype", booking.Turftype);

                connection.Open();
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnValue > 0 ? true : false;
        }
        #endregion

        #region Booking View
        public List<TurfBookingModel> TurfBookingView(string email)
        {
            List<TurfBookingModel> registerList = new List<TurfBookingModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BookingView";
                command.Parameters.AddWithValue("@Email", email);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    registerList.Add(new TurfBookingModel
                    {
                        Username = dataRow["Username"].ToString(),
                        Phone = dataRow["Phone"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Date = Convert.ToDateTime(dataRow["Date"]).Date,
                        Sport = dataRow["Sport"].ToString(),
                        Slots = dataRow["SlotId"].ToString(),
                        Turftype = dataRow["Turftype"].ToString(),
                    });
                }
            }
            return registerList;
        }
        #endregion

        #region Booking View Admin
        public List<TurfBookingModel> TurfBookingViewAdmin()
        {
            List<TurfBookingModel> registerList = new List<TurfBookingModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AdminBookingView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    registerList.Add(new TurfBookingModel
                    {
                        Username = dataRow["Username"].ToString(),
                        Phone = dataRow["Phone"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Date = Convert.ToDateTime(dataRow["Date"]).Date,
                        Sport = dataRow["Sport"].ToString(),
                        Slots = dataRow["SlotId"].ToString(),
                        Turftype = dataRow["Turftype"].ToString(),
                    });
                }
            }
            return registerList;
        }
        #endregion

        #region Venue Add
        public bool VenueDataAdd(VenueModel venue)
        {
            int returnValue = 0;
            string Venuestatus = "Active";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VenueAdd";
                command.Parameters.AddWithValue("@Venuename", venue.Venuename);
                command.Parameters.AddWithValue("@Turftype", venue.Turftype);
                command.Parameters.AddWithValue("@Venuestatus", Venuestatus);

                connection.Open();
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnValue > 0 ? true : false;
        }
        #endregion

        #region Venue View
        public List<VenueModel> VenueDataView()
        {
            List<VenueModel> venueList = new List<VenueModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VenueView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    venueList.Add(new VenueModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Venuename = dataRow["Venuename"].ToString(),
                        Turftype = dataRow["Turftype"].ToString(),
                        Venuestatus = dataRow["Venuestatus"].ToString(),
                    });
                }
            }
            return venueList;
        }
        #endregion

        #region Venue Edit
        public VenueModel VenueDataEdit(int id)
        {
            VenueModel venue = new VenueModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VenueIdView";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    venue.Id = Convert.ToInt32(reader["Id"]);
                    venue.Venuename = reader["Venuename"].ToString();
                    venue.Turftype = reader["Turftype"].ToString();
                    venue.Venuestatus = reader["Venuestatus"].ToString();
                }
                connection.Close();
            }
            return venue;
        }
        #endregion

        #region Venue Update
        public bool VenueDataUpdate(VenueModel venue)
        {
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VenueIdUpdate";
                command.Parameters.AddWithValue("@Id", venue.Id);
                command.Parameters.AddWithValue("@Venuename", venue.Venuename);
                command.Parameters.AddWithValue("@Turftype", venue.Turftype);
                command.Parameters.AddWithValue("@Venuestatus", venue.Venuestatus);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
        }
        #endregion

        #region Venue Delete
        public bool VenueDataDelete(int id)
        {
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "VenueIdDelete";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
        }
        #endregion

        #region Venue Data View
        public List<VenueModel> VenueListView()
        {
            List<VenueModel> venues = new List<VenueModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TurfVenueView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    venues.Add(new VenueModel
                    {
                        Venuename = dataRow["Venuename"].ToString(),
                        Turftype = dataRow["Turftype"].ToString(),
                    });
                }
            }
            return venues;
        }
        #endregion

        #region Holiday View
        public List<HolidayModel> HolidayDataView()
        {
            List<HolidayModel> holidayList = new List<HolidayModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "HolidayView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    holidayList.Add(new HolidayModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        StartDate = Convert.ToDateTime(dataRow["StartDate"]).Date,
                        EndDate = Convert.ToDateTime(dataRow["EndDate"]).Date,
                    });
                }
            }
            return holidayList;
        }
        #endregion

        #region Holiday Add
        public bool HolidayDataAdd(HolidayModel holiday)
        {
            int returnValue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "HolidayAdd";
                command.Parameters.AddWithValue("@StartDate", holiday.StartDate);
                command.Parameters.AddWithValue("@EndDate", holiday.EndDate);

                connection.Open();
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnValue > 0 ? true : false;
        }
        #endregion

        #region Holiday Delete
        public bool HolidayDataDelete(int id)
        {
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "HolidayDelete";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
        }
        #endregion       

        #region Sports View
        public List<SportModel> SportDataAdd()
        {
            List<SportModel> sportList = new List<SportModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SportView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    sportList.Add(new SportModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Name = dataRow["Name"].ToString(),
                        Turftype = dataRow["Turftype"].ToString(),
                    });
                }
            }
            return sportList;
        }
        #endregion

        #region Sports Add
        public bool SportsDataAdd(SportModel sport)
        {
            int returnValue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SportAdd";
                command.Parameters.AddWithValue("@Name", sport.Name);
                command.Parameters.AddWithValue("@Turftype", sport.Turftype);

                connection.Open();
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnValue > 0 ? true : false;
        }
        #endregion

        #region Sports Delete
        public bool SportsDataDelete(int id)
        {
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SportDelete";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
        }
        #endregion    

        #region TimeSlot View
        public List<TimeSlotModel> TimeSlotView()
        {
            List<TimeSlotModel> TimeSlot = new List<TimeSlotModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TimeSlotView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TimeSlot.Add(new TimeSlotModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        SlotName = dataRow["SlotName"].ToString(),
                        SlotTime = dataRow["SlotTime"].ToString(),
                        SlotStatus = dataRow["SlotStatus"].ToString(),
                    });
                }
            }
            return TimeSlot;
        }
        #endregion

        #region TimeSlot Venue View
        public List<TimeSlotModel> TimeSlotVenueView()
        {
            List<TimeSlotModel> TimeSlot = new List<TimeSlotModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TimeSlotVenueView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TimeSlot.Add(new TimeSlotModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        SlotName = dataRow["SlotName"].ToString(),
                        SlotTime = dataRow["SlotTime"].ToString(),
                        SlotStatus = dataRow["SlotStatus"].ToString(),
                    });
                }
            }
            return TimeSlot;
        }
        #endregion

        #region TimeSlot Delete
        public bool TimeSlotDataDelete(int id)
        {
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TimeSlotDelete";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
        }
        #endregion       

        #region TimeSlot Edit
        public TimeSlotModel TimeSlotDataEdit(int id)
        {
            TimeSlotModel timeSlot = new TimeSlotModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SlotIdView";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    timeSlot.Id = Convert.ToInt32(reader["Id"]);
                    timeSlot.SlotName = reader["SlotName"].ToString();
                    timeSlot.SlotTime = reader["SlotTime"].ToString();
                    timeSlot.SlotStatus = reader["SlotStatus"].ToString();
                }
                connection.Close();
            }
            return timeSlot;
        }
        #endregion

        #region TimeSlot Update
        public bool TimeSlotDataUpdate(TimeSlotModel timeSlot)
        {
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "TimeSlotIdUpdate";
                command.Parameters.AddWithValue("@Id", timeSlot.Id);
                command.Parameters.AddWithValue("@SlotName", timeSlot.SlotName);
                command.Parameters.AddWithValue("@SlotTime", timeSlot.SlotTime);
                command.Parameters.AddWithValue("@SlotStatus", timeSlot.SlotStatus);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
        }
        #endregion
    }
}