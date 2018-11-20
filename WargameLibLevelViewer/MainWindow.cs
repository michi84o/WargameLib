using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WargameLib;

namespace WargameLibLevelViewer
{
    public partial class MainWindow : Form
    {
        VOL _vol;
        MapPolygon _selectedMapPolygon;
        MapPolygon SelectedMapPolygon
        {
            get { return _selectedMapPolygon; }
            set
            {
                if (_selectedMapPolygon == value) return;

                var poly = value;
                _selectedMapPolygon = value;
                if (poly == null)
                {
                    tbSelectedPolygon.Text = "";
                    lbTiles.DataSource = null;
                    lbVertices.DataSource = null;
                    return;
                }
                var sb = new StringBuilder();
                sb.AppendLine(poly.Name);
                sb.AppendLine("Type: " + Enum.GetName(typeof(MapPolygonType), poly.Type));
                sb.AppendLine("Center: " + poly.Center);
                sb.AppendLine("Altitude: " + poly.Altitude);
                if (poly.Type == MapPolygonType.Ramp)
                    sb.AppendLine("Altitude Offset: " + poly.AltitudeOffset);
                if (poly.Type == MapPolygonType.Zoom)
                    sb.AppendLine("Zoom: " + poly.Zoom);
                sb.AppendLine("Extra Info: " +
                    poly.ExtraInfo[0] + ";" +
                    poly.ExtraInfo[1] + ";" +
                    poly.ExtraInfo[2] + ";" +
                    poly.ExtraInfo[3] + ";" +
                    poly.ExtraInfo[4] + ";" +
                    poly.ExtraInfo[5] + ";" +
                    poly.ExtraInfo[6] + ";" +
                    poly.ExtraInfo[7]);
                if (poly.Radio != 0)
                    sb.AppendLine("Radio: " + poly.Radio);

                tbSelectedPolygon.Text = sb.ToString();

                lbTiles.DataSource = value.Tiles;
                lbVertices.DataSource = value.Vertices;

            }
        }

        MapTile _selectedTile;
        public MapTile SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                if (_selectedTile == value) return;

                if (value == null)
                {
                    tbSelectedTile.Text = "";
                    return;
                }
                var tile = value;
                _selectedTile = value;

                var sb = new StringBuilder();
                sb.AppendLine(tile.SpriteName);
                sb.AppendLine("Width: " + tile.Width);
                sb.AppendLine("Height: " + tile.Height);
                sb.AppendLine("Position: " + tile.Position);
                sb.AppendLine("Offset: " + tile.Offset);
                sb.AppendLine("Brightness: " + tile.Brightness);
                if (tile.Transformation != MapTileTransformation.None)
                {
                    sb.AppendLine("Transformation:");
                    if ((tile.Transformation & MapTileTransformation.MirrorX) != 0)
                        sb.AppendLine("- MirrorX");
                    if ((tile.Transformation & MapTileTransformation.FlipY) != 0)
                        sb.AppendLine("- FlipY");
                    if ((tile.Transformation & MapTileTransformation.LightOrExplosion) != 0)
                        sb.AppendLine("- LightOrExplosion");
                }

                tbSelectedTile.Text = sb.ToString();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Map File|*.vol"
            };
            var result = dlg.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                var _vol = new VOL(dlg.FileName);
                lbPolygons.DataSource = _vol.Polys;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lbPolygons_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedMapPolygon = lbPolygons.SelectedValue as MapPolygon;
        }

        private void lbTiles_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedTile = lbTiles.SelectedValue as MapTile;
        }
    }
}
