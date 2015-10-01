using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IPCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void init_hostbits()
        {
            for (int i = 1; i < 32; i++)
            {
                Hostbits.Items.Add(i.ToString());
            }
            Hostbits.SelectedItem = "24";
        }

        public MainWindow()
        {
            InitializeComponent();
            init_hostbits();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            byte octet;
            if(!byte.TryParse(textbox.Text,out octet))
            {
                textbox.Text = "";
            }
            else
            {
                if (textbox.Text.Length == 3)
                {
                    TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                    request.Wrapped = true;
                    textbox.MoveFocus(request);
                }
            }
            if(firstOctet.Text!="" && secondOctet.Text!="" && thirdOctet.Text!="" && fourthOctet.Text != "")
            {
                calculate();
            }
            IPCalculation ipc = new IPCalculation(null, 1);
            
        }

        private byte[] text2byte()
        {
            byte[] octets = new byte[4];
            octets[0] = byte.Parse(firstOctet.Text);
            octets[1] = byte.Parse(secondOctet.Text);
            octets[2] = byte.Parse(thirdOctet.Text);
            octets[3] = byte.Parse(fourthOctet.Text);
            return octets;
        }

        private void updateDecimalLabels(IPCalculation ipc)
        {
            lNetmask.Content = ipc.getNetmask();
            lNetwork.Content = ipc.getNetworkAddress();
            lHostNumber.Content = ipc.getHostnumber();
            lHostAddressSize.Content= ipc.getHostBits()+" Bits";
            lNetworkAddressSize.Content = ipc.getNetworkBits()+" Bits";
            lStartAddress.Content = ipc.getfirstAddress();
            lEndAddress.Content = ipc.getLastAddress();
            lBroadcastAddress.Content = ipc.getBroadcastAddress();
            
        }

        private void updateBinaryLabels(IPCalculation ipc)
        {
            lNetmaskBinary.Content = ipc.getNetmask().ToBinaryString();
            lNetworkBinary.Content = ipc.getNetworkAddress().ToBinaryString();
            lStartAddressBinary.Content = ipc.getfirstAddress().ToBinaryString();
            lEndAddressBinary.Content = ipc.getLastAddress().ToBinaryString();
            lBroadcastAddressBinary.Content = ipc.getBroadcastAddress().ToBinaryString();
        }

        private void calculate()
        {
            byte[] octets= text2byte();
            //InternetProtocolAddress ip = new InternetProtocolAddress(octets[0], octets[1], octets[2], octets[3]);
            InternetProtocolAddress ip = new InternetProtocolAddress(192,168,178,222);
            IPCalculation ipc = new IPCalculation(ip, byte.Parse(Hostbits.SelectedItem.ToString()));
            updateDecimalLabels(ipc);
            updateBinaryLabels(ipc);
        }

        private void Hostbits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstOctet.Text != "" && secondOctet.Text != "" && thirdOctet.Text != "" && fourthOctet.Text != "")
            {
                calculate();
            }
        }
    }
}
