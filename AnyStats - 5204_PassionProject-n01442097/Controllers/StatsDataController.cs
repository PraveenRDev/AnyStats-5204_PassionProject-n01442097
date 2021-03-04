using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AnyStats___5204_PassionProject_n01442097.Controllers;
using AnyStats___5204_PassionProject_n01442097.Models;
using Microsoft.AspNet.Identity;
using static AnyStats___5204_PassionProject_n01442097.Models.Coordinate;

namespace AnyStats___5204_PassionProject_n01442097.App_Start
{
    public class StatsDataController : ApiController
    {
        // instance of CoordinatesDataController 
        private CoordinatesDataController coorController = new CoordinatesDataController();

        // database access point
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets a list of publicly shared Stats in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Stats including their ID and Name.</returns>
        /// <example>
        /// GET: api/StatsData/GetPublicStats/20/5
        /// </example>
        [Route("api/StatsData/GetPublicStats/{StartIndex}/{PerPage}")]
        [ResponseType(typeof(IEnumerable<StatDto>))]
        public IHttpActionResult GetPublicStats(int StartIndex, int PerPage)
        {
            // Get all public Statistics
            List<Stat> Statistics = db.Stats.Where(Stat => Stat.isPublic == true).OrderBy(s => s.StatId).Skip(StartIndex).Take(PerPage).ToList();
            List<StatDto> StatsDtos = new List<StatDto> { };

            foreach(var Stat in Statistics)
            {
                StatDto NewStat = new StatDto
                {
                    StatId = Stat.StatId,
                    StatName = Stat.StatName
                };
                StatsDtos.Add(NewStat);
            }
            return Ok(StatsDtos);
        }

        /// <summary>
        /// Gets the count of publicly shared Stats in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>An integer showing the count of public stats.</returns>
        /// <example>
        /// GET: api/StatsData/GetPublicStatsCount
        /// </example>
        [ResponseType(typeof(int))]
        public IHttpActionResult GetPublicStatsCount()
        {
            // Get all public Statistics
            var count = db.Stats.Where(Stat => Stat.isPublic == true).Count();
            return Ok(count);
        }

        /// <summary>
        /// Gets a list of public Stats along with stats created by logged in user in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Stats(public and private) including their ID and Name.</returns>
        /// <example>
        /// GET: api/StatsData/GetAllStats/1743c536-aaed-4c36-8363-d521dbfaee16/20/5
        /// </example>
        [Route("api/StatsData/GetAllStats/{authorId}/{StartIndex}/{PerPage}")]
        [ResponseType(typeof(IEnumerable<StatDto>))]
        public IHttpActionResult GetAllStats(string authorId, int StartIndex, int PerPage)
        {
            // Get all public Statistics
            List<Stat> Statistics = db.Stats.Where(Stat => Stat.isPublic == true || Stat.AuthorId == authorId).OrderBy(s => s.StatId).Skip(StartIndex).Take(PerPage).ToList();
            List<StatDto> StatsDtos = new List<StatDto> { };

            foreach (var Stat in Statistics)
            {
                StatDto NewStat = new StatDto
                {
                    StatId = Stat.StatId,
                    StatName = Stat.StatName
                };
                StatsDtos.Add(NewStat);
            }
            return Ok(StatsDtos);
        }


        /// <summary>
        /// Gets the count of public Stats along with stats created by logged in user in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>An integer showing the count of Stats(public and private).</returns>
        /// <example>
        /// GET: api/StatsData/GetAllStatsCount
        /// </example>
        [Route("api/StatsData/GetAllStatsCount/{authorId}")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetAllStatsCount(string authorId)
        {
            // Get all public Statistics
            var count = db.Stats.Where(Stat => Stat.isPublic == true || Stat.AuthorId == authorId).Count();
            return Ok(count);
        }


        /// <summary>
        /// Adds stats to database.
        /// </summary>
        /// <param name="Stat">A Stat object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/StatsData/AddStat
        /// FORM DATA: Stat JSON Object
        /// </example>
        [ResponseType(typeof(Stat))]
        [HttpPost]
        public IHttpActionResult AddStat([FromBody] StatDto Stat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Stat newStat = new Stat
            {
                StatName = Stat.StatName,
                StatDescription = Stat.StatDescription,
                XAxis = Stat.XAxis,
                YAxis = Stat.YAxis,
                isPublic = Stat.isPublic,
                DateCreated = DateTime.Now,
                AuthorId = Stat.AuthorId
            };

            // add new stat to db
            db.Stats.Add(newStat);
            db.SaveChanges();

            // insert associated coordinates of the stats to db
            coorController.InsertCoordinates(newStat.StatId, Stat.XValue, Stat.YValue);

            return Ok(newStat.StatId);
        }

        /// <summary>
        /// Finds a particular stat in the database with a 200 status code. If the stat is not found, return 404.
        /// </summary>
        /// <param name="id">The stat id</param>
        /// <returns>Information about the stat, including stat id, name, description and x-axis, y-axis, author id and date created</returns>
        /// <example>
        /// GET: api/StatsData/GetStat/5
        /// </example>
        [ResponseType(typeof(StatDto))]
        public IHttpActionResult GetStat(int id)
        {
            Stat Stat = db.Stats.Find(id);
            if (Stat == null)
            {
                return NotFound();
            }
            StatDto StatDto = new StatDto
            {
                StatId = Stat.StatId,
                StatName = Stat.StatName,
                StatDescription = Stat.StatDescription,
                XAxis = Stat.XAxis,
                YAxis = Stat.YAxis,
                AuthorId = Stat.AuthorId,
                isPublic = Stat.isPublic,
                DateCreated= Stat.DateCreated
            };

            return Ok(StatDto);
        }

        /// <summary>
        /// Deletes a Stat in the database
        /// </summary>
        /// <param name="id">The id of the stat to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// GET: api/StatsData/DeleteStat/5
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteStat(int id)
        {
            Stat stat = db.Stats.Find(id);

            if (stat == null)
            {
                return NotFound();
            }
            db.Stats.Remove(stat);

            // delete coordinates associated to the deleted stat
            coorController.DeleteCoordinates(id);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates a Stat in the database given information about the stat.
        /// </summary>
        /// <param name="id">The stat id</param>
        /// <param name="Stat">A Stat object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/StatsData/UpdateStat/5
        /// FORM DATA: Stat JSON Object
        /// </example>
        public IHttpActionResult UpdateStat(int id, [FromBody] StatDto Stat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Stat.StatId)
            {
                return BadRequest();
            }
            Stat Statistics = new Stat()
            {
                StatId = Stat.StatId,
                StatName = Stat.StatName,
                StatDescription = Stat.StatDescription,
                XAxis = Stat.XAxis,
                YAxis = Stat.YAxis,
                isPublic = Stat.isPublic,
                AuthorId = Stat.AuthorId,
                DateCreated = Stat.DateCreated
            };

            // update stats
            db.Entry(Statistics).State = EntityState.Modified;

            try
            {
                // Replace exisiting coordinates with current coordinates
                coorController.DeleteCoordinates(Stat.StatId);
                coorController.InsertCoordinates(Stat.StatId, Stat.XValue, Stat.YValue);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatExists(Stat.StatId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Finds a stat in the system. Internal use only.
        /// </summary>
        /// <param name="id">The stat id</param>
        /// <returns>TRUE if the stat exists, false otherwise.</returns>
        private bool StatExists(int id)
        {
            return db.Stats.Count(e => e.StatId == id) > 0;
        }
    }
}