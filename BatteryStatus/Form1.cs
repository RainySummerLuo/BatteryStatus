using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

namespace BatteryStatus {
    public partial class Form1 : Form {
        Dictionary<ushort, string> StatusCodes;
        Dictionary<ushort, string> StatusDescription;
        Dictionary<ushort, string> BatteryChemistry;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            StatusCodes = new Dictionary<ushort, string> {
                { 1, "Other (1)" },
                { 2, "Unknown (2)" },
                { 3, "Fully Charged (3)" },
                { 4, "Low (4)" },
                { 5, "Critical (5)" },
                { 6, "Charging (6)" },
                { 7, "Charging and High (7)" },
                { 8, "Charging and Low (8)" },
                { 9, "Charging and Critical (9)" },
                { 10, "Undefined (10)" },
                { 11, "Partially Charged (11)" }
            };

            StatusDescription = new Dictionary<ushort, string> {
                { 1, "The battery is discharging." },
                { 2, "The system has access to AC so no battery is being discharged. However, the battery is not necessarily charging." },
            };

            BatteryChemistry = new Dictionary<ushort, string> {
                { 1, "Other (1)" },
                { 2, "Unknown (2)" },
                { 3, "Lead Acid (3)" },
                { 4, "Nickel Cadmium (4)" },
                { 5, "Nickel Metal Hydride (5)" },
                { 6, "Lithium-ion (6)" },
                { 7, "Zinc air (7)" },
                { 8, "Lithium Polymer (8)" },
            };

            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery");

            string caption = null, name = null, status = null, statusDescription = null, batteryChemistry = null;

            foreach (ManagementObject mo in mos.Get()) {
                caption = mo["Caption"].ToString();

                name = mo["Name"].ToString();

                ushort statuscode = (ushort)mo["BatteryStatus"];

                ushort batterychemistrycode = (ushort)mo["Chemistry"];

                status = StatusCodes[statuscode];

                if (StatusDescription.ContainsKey(statuscode)) {
                    statusDescription = StatusDescription[statuscode];
                } else {
                    statusDescription = "[None]";
                }

                batteryChemistry = BatteryChemistry[batterychemistrycode];
            }

            Text = "Battery Status - " + caption;
            lblBatteryName.Text = "Battery Name:   " + name + " / Chemistry:   " + batteryChemistry;
            lblStatus.Text = status;
            lblStatusDescription.Text = statusDescription;
        }
    }
}
