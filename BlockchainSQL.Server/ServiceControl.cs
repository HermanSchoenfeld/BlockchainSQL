using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sphere10.Framework;
using Sphere10.Framework.Scheduler;
using Sphere10.Windows.WinForms;

namespace BlockchainSQL.Server {
    public partial class ServiceStatusControl : UserControlEx {
        private ServiceController _serviceController;
        private ServiceStatus _serviceStatus;
        public ServiceStatusControl() {
            InitializeComponent();
        }

        public ServiceStatus Status {
            get { return _serviceStatus; }
            set {
                _serviceStatus = value;
                switch (_serviceStatus) {
                    case ServiceStatus.NotInstalled:
                        _trafficLight.BackColor = Color.Gray;
                        _trafficLightLabel.Text = "Not installed";
                        _serviceDetailLabel.Text = "No BlockchainSQL service was detected running on this machine";
                        _serviceButton.Visible = false;
                        break;
                    case ServiceStatus.Starting:
                        _trafficLight.BackColor = Color.Orange;
                        _trafficLightLabel.Text = "Starting";
                        _serviceDetailLabel.Text = "Service is starting";
                        _serviceButton.Visible = false;
                        break;
                    case ServiceStatus.Started:
                        _trafficLight.BackColor = Color.Green;
                        _trafficLightLabel.Text = "Starting";
                        _serviceDetailLabel.Text = _serviceController.DisplayName + " is running";
                        _serviceButton.Text = "Stop";
                        _serviceButton.Visible = true;                        
                        break;
                    case ServiceStatus.Stopping:
                        _trafficLight.BackColor = Color.Orange;
                        _trafficLightLabel.Text = "Stopping";
                        _serviceDetailLabel.Text = "Service is stopping";
                        _serviceButton.Visible = false;
                        break;
                    case ServiceStatus.Stopped:
                        _trafficLight.BackColor = Color.Red;
                        _trafficLightLabel.Text = "Stopped";
                        _serviceDetailLabel.Text = _serviceController.DisplayName + " is stopped";
                        _serviceButton.Text = "Start";
                        _serviceButton.Visible = true;
                        break;
                    case ServiceStatus.Error:
                        _trafficLight.BackColor = Color.Red;
                        _trafficLightLabel.Text = "Error";
                        _serviceButton.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Start() {
            Scheduler.Global.AddJob(
                JobBuilder
                    .For(MonitorService)                    
                    .Repeat.OnInterval(DateTime.Now, TimeSpan.FromSeconds(2))
                    .RunSyncronously()
                    .Build()
                );
        }

        private void MonitorService() {
            try {
                _serviceController = Tools.BlockchainSQL.GetServiceController();
                if (_serviceController == null) {
                    Status = ServiceStatus.NotInstalled;
                    return;
                }

                switch (_serviceController.Status) {
                    case ServiceControllerStatus.Stopped:
                        Status = ServiceStatus.Stopped;
                        break;
                    case ServiceControllerStatus.StartPending:
                        Status = ServiceStatus.Starting;
                        break;
                    case ServiceControllerStatus.StopPending:
                        Status = ServiceStatus.Stopping;
                        break;
                    case ServiceControllerStatus.Running:
                        Status = ServiceStatus.Started;
                        break;
                    case ServiceControllerStatus.ContinuePending:
                    case ServiceControllerStatus.PausePending:
                    case ServiceControllerStatus.Paused:
                    default:
                        Status = ServiceStatus.Error;
                        break;
                }

            } catch (Exception error) {
                var x = 1;
            }
        }




    }
}
