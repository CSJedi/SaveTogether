using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;

namespace SaveTogether.Models
{
    public class SaveTogetherDbInitializer : DropCreateDatabaseAlways<SaveTogetherContext>
    {
        protected override void Seed(SaveTogetherContext db)
        {
            db.Regions.Add(new Region
            {
                Name = "Forest near the villages of Madirobe ",
                Population = 50 ,
                Description = "This region is small remaining patche in Madagascar’s far northern regions."
            });
            db.Regions.Add(new Region
            {
                Name = "Forest near the villages of Ankarongana",
                Population = 65,
                Description = "This region locate on Madagascar’s far north in the Sahafary region."
            });
            db.Regions.Add(new Region
            {
                Name = "In the vicinity of Andrahona",
                Population = 75,
                Description = "A small mountain emerging out of the surrounding lowlands about 30 km south of Antsiranana. This species has only been recorded at elevations below 300 m."
            });
            base.Seed(db);
        }
    }
}