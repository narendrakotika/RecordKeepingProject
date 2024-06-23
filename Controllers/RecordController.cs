using Newtonsoft.Json;
using RecordKeepingProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace RecordKeeper.Controllers
{
    public class RecordController : Controller
    {
        private string recordsFilePath = HostingEnvironment.MapPath("~/App_Data/records.json");

        public ActionResult Index()
        {
            List<Record> records = GetRecordsFromFile();
            return View(records);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Record record)
        {
            if (ModelState.IsValid)
            {
                List<Record> records = GetRecordsFromFile();
                record.Id = records.Count + 1;
                record.Date = DateTime.Now;
                records.Add(record);
                SaveRecordsToFile(records);
                return RedirectToAction("Index");
            }
            return View(record);
        }

        public ActionResult Edit(int id)
        {
            List<Record> records = GetRecordsFromFile();
            Record record = records.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                return View(record);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Record record)
        {
            if (ModelState.IsValid)
            {
                List<Record> records = GetRecordsFromFile();
                Record existingRecord = records.FirstOrDefault(r => r.Id == record.Id);
                if (existingRecord != null)
                {
                    existingRecord.Title = record.Title;
                    existingRecord.Description = record.Description;
                    existingRecord.Category = record.Category;
                    SaveRecordsToFile(records);
                }
                return RedirectToAction("Index");
            }
            return View(record);
        }

        public ActionResult Delete(int id)
        {
            List<Record> records = GetRecordsFromFile();
            Record record = records.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                records.Remove(record);
                SaveRecordsToFile(records);
            }
            return RedirectToAction("Index");
        }

        private List<Record> GetRecordsFromFile()
        {
            string jsonData = System.IO.File.ReadAllText(recordsFilePath);
            return JsonConvert.DeserializeObject<List<Record>>(jsonData);
        }

        private void SaveRecordsToFile(List<Record> records)
        {
            string jsonData = JsonConvert.SerializeObject(records, Formatting.Indented);
            System.IO.File.WriteAllText(recordsFilePath, jsonData);
        }
    }
}
