using AssetManagementWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AssetManagementWeb.Models;
using System.Globalization;

namespace AssetManagementWeb.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }


        // POST: Asset/Create
        [HttpPost]
        public JsonResult AssignLocation()
        {
            string json = Request.InputStream.ReadToEnd();
            AssetLocationModel inputData =
                JsonConvert.DeserializeObject<AssetLocationModel>(json);

            bool success = false;
            string error = "";

            AssetManagementEntities entities = new AssetManagementEntities();
            try
            {
                //haetaan ensin paikan id-numero koodin perusteella
                int locationId = (from l in entities.Location
                                  where l.Code == inputData.LocationCode
                                  select l.LocationId).FirstOrDefault();

                //haetaan ensin laitteen id-numero koodin perusteella
                int assetId = (from a in entities.Asset
                               where a.Code == inputData.AssetCode
                               select a.AssetId).FirstOrDefault();

                if ((locationId > 0) && (assetId > 0))
                {
                    //tallennetaan uusi rivi aikaleiman kanssa kantaan
                    AssetLocations newEntry = new AssetLocations();
                    newEntry.LocationId = locationId;
                    newEntry.AssetId = assetId;
                    newEntry.LastSeen = DateTime.Now;

                    entities.AssetLocations.Add(newEntry);
                    entities.SaveChanges();

                    success = true;

                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }

            //palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }


        // GET: Asset/Edit/5
        public ActionResult List()
        {
            List<LocatedAssetsViewModel> model
                = new List<LocatedAssetsViewModel>();

            AssetManagementEntities entities = new AssetManagementEntities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                //muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFI = new CultureInfo("fi-FI");
                foreach (AssetLocations asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.AssetLocationId;
                    view.LocationCode = asset.Location.Code;
                    view.LocationName = asset.Location.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + ": " + asset.Asset.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString("dd.MM.yyyy");
                    //view.LastSeen = asset.LastSeen.Value.ToString("fiFI"); //[vaihtoehto]

                    model.Add(view);
                }
            }

            finally
            {
                entities.Dispose();
            }

                return View(model);
        }

        public ActionResult ListJson()
        {
            List<LocatedAssetsViewModel> model
                = new List<LocatedAssetsViewModel>();

            AssetManagementEntities entities = new AssetManagementEntities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                //muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFI = new CultureInfo("fi-FI");
                foreach (AssetLocations asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.AssetLocationId;
                    view.LocationCode = asset.Location.Code;
                    view.LocationName = asset.Location.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + ": " + asset.Asset.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString("dd.MM.yyyy");
                    //view.LastSeen = asset.LastSeen.Value.ToString("fiFI"); //[vaihtoehto]

                    model.Add(view);
                }
            }

            finally
            {
                entities.Dispose();
            }

            return Json(model,JsonRequestBehavior.AllowGet);
        }

    }
}
