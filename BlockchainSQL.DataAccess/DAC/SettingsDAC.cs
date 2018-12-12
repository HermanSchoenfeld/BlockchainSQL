using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC {
        public long CountSettings() {
            return this.Count("Settings");
        }

        public IEnumerable<Setting> GetSettings() {
            return
                (this.Select("Settings", new[] { "ID", "Name", "Value" }))
                .Rows
                .Cast<DataRow>().Select(Hydrators.HydrateSettings);
        }

        public Setting GetSetting(KnownSettings knownSetting) {
            return this.GetSettingByID((int)knownSetting);
        }

        public Setting GetSettingByID(int id) {
            var results = (this.Select("Settings", new[] { "ID", "Name", "Value" })).Rows.Cast<DataRow>().ToArray();
            if (results.Length != 1)
                throw new NoSingleRecordException("Settings", id, results.Length);
            return Hydrators.HydrateSettings(results[0]);
        }

        public void Update(Setting settings) {
            this.Update(
                "Settings",
                new[] {
                    new ColumnValue("Name", settings.Name),
                    new ColumnValue("Value", settings.Value),
                },
                whereValues: new[] {
                    new ColumnValue("ID", settings.ID), 
                }
            );
        }

        public virtual T GetSettingValue<T>(int id) {
            var rows =
                (this.Select("Settings", new[] { "Value" }, columnMatches: new[] { new ColumnValue("ID", id) }))
                    .Rows
                    .Cast<DataRow>()
                    .ToArray();
            if (rows.Length != 1)
                throw new NoSingleRecordException("Settings", id, rows.Length);
            return rows[0].Get<T>("Value");
        }

        public virtual void SetSettingValue(int id, object value) {
            object dbValue;
            if (value == null || value == DBNull.Value)
                dbValue = DBNull.Value;
            else
                dbValue = (string)Convert.ChangeType(value, typeof(string));

            this.Update(
                "Settings",
                new[] { new ColumnValue("Value", dbValue) },
                whereValues:
                new[] { new ColumnValue("ID", id), }
            );
        }
    }
}
