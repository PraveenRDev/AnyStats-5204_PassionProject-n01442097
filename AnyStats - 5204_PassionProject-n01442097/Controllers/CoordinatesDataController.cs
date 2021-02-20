using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AnyStats___5204_PassionProject_n01442097.Models;
using static AnyStats___5204_PassionProject_n01442097.Models.Coordinate;

namespace AnyStats___5204_PassionProject_n01442097.Controllers
{
    public class CoordinatesDataController : ApiController
    {
        // database access point
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Finds all coordinates associated to a given stat id in the database with a 200 status code. If  coordinates are not found, return 404.
        /// </summary>
        /// <param name="id">The stat id</param>
        /// <returns>Information about the Coordinates, including all the x and y values as lists</returns>
        // <example>
        // GET: api/CoordinatesData/FindCoordinatesForStats/1
        // </example>
        [HttpGet]
        [ResponseType(typeof(CoordinateDto))]
        public IHttpActionResult FindCoordinatesForStats(int id)
        {
            // filter all the coordinates of the particular stat id
            var Coordinates = db.Coordinates
                .Where(coordinate => coordinate.StatId == id);

            if (Coordinates == null)
            {
                return NotFound();
            }

            List<string> XValues = new List<string>();
            List<double> YValues = new List<double>();
            CoordinateDto StatCoordinates = new CoordinateDto();

            // add coordinates separatly as x and y
            foreach (var Coordinate in Coordinates)
            {
                XValues.Add(Coordinate.XValue);
                YValues.Add(Coordinate.YValue);
            }
            StatCoordinates.StatId = id;
            StatCoordinates.XValues = XValues;
            StatCoordinates.YValues = YValues;

            return Ok(StatCoordinates);
        }

        /// <summary>
        /// Insert coordinates associated to particular stats to db
        /// </summary>
        /// <param name="StatId">The stat id</param>
        /// <param name="XValue">List of X Values</param>
        /// <param name="YValue">List of Y Values</param>
        internal void InsertCoordinates(int StatId, List<string>XValue, List<double> YValue)
        {
            // get the count of coordinates(pair)
            int coordinateLength = XValue.Count();

            // insert each pair of coordinates to db with associated stat id
            for (int index = 0; index < coordinateLength; index++)
            {
                Coordinate NewCoordinate = new Coordinate
                {
                    XValue = XValue[index],
                    YValue = YValue[index],
                    StatId = StatId
                };
                db.Coordinates.Add(NewCoordinate);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Delete coordinates associated to particular stats from db
        /// </summary>
        /// <param name="StatId">The stat id</param>
        internal void DeleteCoordinates(int StatId)
        {
            // filter all the coordinates associated to a particular stat
            var Coord = db.Coordinates.Where(c => c.StatId == StatId);
            if (Coord != null)
            {
                db.Coordinates.RemoveRange(Coord);
                db.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoordinateExists(int id)
        {
            return db.Coordinates.Count(e => e.CoordinateId == id) > 0;
        }
    }
}