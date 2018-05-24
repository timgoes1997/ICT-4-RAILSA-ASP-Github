using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Rails.Scripts;

namespace ICT4Rails
{
    public partial class frmAlgemeen : Form
    {
        #region Fields

        // Fields
        private BeheerController bc;
        private SchoonmaakController sc;
        private ReparatieController rc;
        private IUController ic;
        private string errorselect = "Niet alle vereiste informatie geselecteerd.";

        #endregion

        public frmAlgemeen()
        {
            InitializeComponent();
            bc = new BeheerController();
            ic = new IUController();
            sc = new SchoonmaakController();
            rc = new ReparatieController();
            cbReparatieType.DataSource = Enum.GetValues(typeof(ReparatieType));
            cbSchoonmaakType.DataSource = Enum.GetValues(typeof(SchoonmaakType));
            cbTypeTramToevoegen.DataSource = Enum.GetValues(typeof(TramType));
            cbTramstatusTramstatusAanpassen.DataSource = Enum.GetValues(typeof(TramStatus));
            cbInUitRijTramstatus.DataSource = Enum.GetValues(typeof(TramStatus));
            FillAllLists();
            FillGUIsporen();
            FillDgvReservering();

        }

        #region Algemeen

        private void tcAlgemeen_Click(object sender, EventArgs e)
        {
            FillGUIsporen();
            FillAllLists();
            // Reserveringen dgv
            FillDgvReservering();
        }

        private void FillAllLists()
        {
            #region Fill all combobox lists
            FillVerplaatsenTramLijst();
            FillVerplaatsenSpoorLijst();
            cbTramnummerTramstatusAanpassen.Items.Clear();
            cbInUitRijTram.Items.Clear();
            cbTramVerwijderen.Items.Clear();
            cbTramSectorReserveren.Items.Clear();
            cbTramnummerServiceAfronden.Items.Clear();
            cbTramnummerSchoonmaakAdd.Items.Clear();
            cbSpoorSectorReserveren.Items.Clear();
            cbSpoorSectorblokkeren.Items.Clear();
            cbTramnummerServiceAdd.Items.Clear();
            cbTramnummerSchoonmaakAfronden.Items.Clear();
            cbVerwijderReservering.Items.Clear();

            foreach (Tram tram in bc.Trams)
            {
                cbTramnummerTramstatusAanpassen.Items.Add(tram.TramNummer);
                cbInUitRijTram.Items.Add(tram.TramNummer);
                cbTramVerwijderen.Items.Add(tram.TramNummer);
                cbTramSectorReserveren.Items.Add(tram.TramNummer);
            }

            foreach (Tram t in sc.GetViezeTrams())
            {
                cbTramnummerSchoonmaakAdd.Items.Add(t.TramNummer);
            }

            foreach (Tram t in rc.GetKapotteTrams())
            {
                cbTramnummerServiceAdd.Items.Add(t.TramNummer);
            }

            foreach (Spoor spoor in bc.Sporen)
            {
                cbSpoorSectorReserveren.Items.Add(spoor.SpoorNummer);
                cbSpoorSectorblokkeren.Items.Add(spoor.SpoorNummer);
            }

            foreach (Schoonmaak s in sc.Schoonmaak)
            {
                cbTramnummerSchoonmaakAfronden.Items.Add(s.Tram.TramNummer);
            }

            foreach (Reparatie r in rc.Reparatie)
            {
                cbTramnummerServiceAfronden.Items.Add(r.Tram.TramNummer);
            }

            foreach (Reservering r in bc.GetAllReserveringen())
            {
                cbVerwijderReservering.Items.Add(r.ToString());
            }

            #endregion
        }

        private void UpdateControls()
        {
            foreach (Control c in this.Controls)
            {
                c.Update();
            }
        }

        /// <summary>
        /// Vult de combobox met verplaatsbare trams.
        /// </summary>
        private void FillVerplaatsenTramLijst()
        {
            cbTramTramVerplaatsen.Items.Clear();
            List<Tram> verplaatsbaar = bc.VerplaatsbareTrams();
            if (verplaatsbaar != null)
            {
                cbTramTramVerplaatsen.Enabled = true;
                foreach (Tram tram in verplaatsbaar)
                {
                    cbTramTramVerplaatsen.Items.Add(tram.TramNummer);
                }
            }
            else
            {
                cbTramTramVerplaatsen.Enabled = false;
            }
        }

        /// <summary>
        /// Vult de combobox met sporen die vrije sectoren hebben.
        /// </summary>
        private void FillVerplaatsenSpoorLijst()
        {
            cbSpoorTramVerplaatsen.Items.Clear();
            List<Spoor> vrij = bc.VrijeSporen();
            if (vrij != null)
            {
                cbSpoorTramVerplaatsen.Enabled = true;
                foreach (Spoor spoor in vrij)
                {
                    cbSpoorTramVerplaatsen.Items.Add(spoor.SpoorNummer);
                }
            }
            else
            {
                cbSpoorTramVerplaatsen.Enabled = false;
            }
        }

        private void Algemeen_Load(object sender, EventArgs e)
        {
            // Beheersysteem
            VulAlgemeneDGV();

            #region Reservering dgv

            #endregion

            // In- en uitrijsysteem dummydata
            dgvInEnUitrijsysteem.Columns.Add("col", "Tram");
            dgvInEnUitrijsysteem.Columns.Add("col", "Spoor");
            dgvInEnUitrijsysteem.Columns.Add("col", "Sector");
            dgvInEnUitrijsysteem.Columns.Add("col", "Status");
            dgvInEnUitrijsysteem.Columns.Add("col", "Vertrektijd");
        }

        #endregion

        #region Schoonmaak

        private void btnSchoonmaaklijstOpvragen_Click(object sender, EventArgs e)
        {
            UpdateSchoonmaak();
        }

        private void UpdateSchoonmaak()
        {
            dgvSchoonmaaklijst.Rows.Clear();
            dgvSchoonmaaklijst.Columns.Clear();


            dgvSchoonmaaklijst.Columns.Add("col1", "Tramnummer");

            dgvSchoonmaaklijst.Columns.Add("col2", "Type");

            dgvSchoonmaaklijst.Columns.Add("col3", "Opmerking");
            dgvSchoonmaaklijst.Columns.Add("col4", "Datum");

            List<Schoonmaak> sl = sc.GetAllSchoonmaak();

            foreach (Schoonmaak s in sl)
            {
                dgvSchoonmaaklijst.Rows.Add(s.Tram.TramNummer, s.SchoonmaakType, s.Opmerking, s.Datum);
            }
        }

        private void btnAfronden_Click(object sender, EventArgs e)
        {

            int nummer = Convert.ToInt32(cbTramnummerSchoonmaakAfronden.SelectedItem.ToString());

            sc.SchoonmaalAfronden(nummer);

            UpdateSchoonmaak();
            FillAllLists();
        }

        private void btnAddSchoonmaak_Click(object sender, EventArgs e)
        {
            int tramnummer = Convert.ToInt32(cbTramnummerSchoonmaakAdd.SelectedItem.ToString());
            SchoonmaakType k;
            Enum.TryParse(cbSchoonmaakType.SelectedItem.ToString(), out k);

            string opmerking = tbOpmerkingSchoonmaak.Text;

            Tram t = bc.GetTramByNummer(tramnummer);
            if (t != null)
            {
                MessageBox.Show(sc.AddSchoonmaak(t, opmerking, DateTime.Now, false, k));
            }
            else
            {
                MessageBox.Show("Kon de tram niet vinden bij dit nummer.");
            }
            UpdateSchoonmaak();
            FillAllLists();
        }

        #endregion 

        #region Reparatie

        private void btnReparatieLijstOpvragen_Click(object sender, EventArgs e)
        {
            UpdateReparatie();
        }

        private void UpdateReparatie()
        {
            dgvReparatieLijst.Rows.Clear();
            dgvReparatieLijst.Columns.Clear();


            dgvReparatieLijst.Columns.Add("col1", "Tramnummer");

            dgvReparatieLijst.Columns.Add("col2", "Type");

            dgvReparatieLijst.Columns.Add("col3", "Opmerking");
            dgvReparatieLijst.Columns.Add("col4", "Datum");

            List<Reparatie> rl = rc.GetAllReparatie();

            foreach (Reparatie s in rl)
            {
                dgvReparatieLijst.Rows.Add(s.Tram.TramNummer, s.ReparatieType, s.Opmerking, s.Datum);
            }
        }

        private void btnServicebeurtPlaatsen_Click(object sender, EventArgs e)
        {
            int tramnummer = Convert.ToInt32(cbTramnummerServiceAdd.SelectedItem.ToString());
            ReparatieType r;
            Enum.TryParse(cbReparatieType.SelectedItem.ToString(), out r);
            string opmerking = tbOpmerkingenServicebeurt.Text;

            Tram t = bc.GetTramByNummer(tramnummer);
            if (t != null)
            {
                MessageBox.Show(rc.AddReparatie(r, t, opmerking, DateTime.Now, false));
            }
            UpdateReparatie();
            FillAllLists();
        }

        private void btnReparatieAfronden_Click(object sender, EventArgs e)
        {
            int nummer = Convert.ToInt32(cbTramnummerServiceAfronden.SelectedItem.ToString());

            rc.ReparatieAfronden(nummer);

            UpdateReparatie();
            FillAllLists();
        }

        #endregion 

        #region Beheer

        private void btnStartSimulatie_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int rng;
            List<Tram> tramsInDienst = bc.TramsInDienst();
            Random random = new Random();
            rng = random.Next(0, (tramsInDienst.Count));

            if (!bc.TramInDienst())
            {
                timer1.Stop();
                MessageBox.Show("Alle trams zijn ingedeeld");
                return;
            }

            if (tramsInDienst[rng] != null)
            {
                if (tramsInDienst[rng].Status == TramStatus.InDienst)
                {
                    ic.DeelTramInBeter(tramsInDienst[rng]);
                    FillGUIsporen();
                }
            }

        }

        private void btnBevestigToevoegen_Click(object sender, EventArgs e)
        {
            int tramnummer = Convert.ToInt32(tbNummerTramToevoegen.Text);
            string vertrektijd = tbVertrektijdTramToevoegen.Text;

            TramType t;
            Enum.TryParse(cbTypeTramToevoegen.SelectedItem.ToString(), out t);

            MessageBox.Show(bc.TramToevoegen(tramnummer, vertrektijd, t));
        }

        private void VulAlgemeneDGV()
        {
            List<Spoor> listSporen = bc.GetAllSporen();

            int huidigeMaxSectoren = 0;
            foreach (Spoor sporen in listSporen) //test
            {
                if (sporen.Sectoren.Count > huidigeMaxSectoren)
                {
                    huidigeMaxSectoren = sporen.Sectoren.Count;
                }
            }

            foreach (Spoor s in listSporen)
            {
                dgvBeheer.Columns.Add("idk", "Spoor " + s.SpoorNummer);

                //for (int i = 0; i < s.Sectoren.Count - 1; i++)
                //{
                //    dgvBeheer.Rows[i].Cells[0].Value = s.Sectoren[i].Id;
                //}
            }
            for (int i = 0; i < huidigeMaxSectoren; i++)
            {
                dgvBeheer.Rows.Add(1);
            }

            foreach (DataGridViewRow row in dgvBeheer.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.LightGray;
                    cell.Style.ForeColor = Color.DarkGray;
                }
            }

            for (int rowIndex = 0; rowIndex < huidigeMaxSectoren; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < listSporen.Count; columnIndex++)
                {
                    if (listSporen[columnIndex].Sectoren.Count > rowIndex)
                    {
                        dgvBeheer.Rows[rowIndex].Cells[columnIndex].Style.BackColor =
                            dgvBeheer.Rows[rowIndex].Cells[columnIndex].OwningColumn.DefaultCellStyle.BackColor;
                        dgvBeheer.Rows[rowIndex].Cells[columnIndex].Style.ForeColor =
                            dgvBeheer.Rows[rowIndex].Cells[columnIndex].OwningColumn.DefaultCellStyle.ForeColor;
                    }
                }
            }
        }

        private void btnStatusAanpassen_Click(object sender, EventArgs e)
        {
            if (cbTramnummerTramstatusAanpassen.SelectedItem != null &&
                cbTramstatusTramstatusAanpassen.SelectedItem != null)
            {
                Tram tram = bc.GetTramByNummer(Convert.ToInt32(cbTramnummerTramstatusAanpassen.SelectedItem.ToString()));
                if (tram.Status == (TramStatus)cbTramstatusTramstatusAanpassen.SelectedItem)
                {
                    MessageBox.Show("Deze tram heeft al de geselecteerde status.");
                }
                else
                {
                    MessageBox.Show(bc.TramStatusAanpassen(tram, (TramStatus)cbTramstatusTramstatusAanpassen.SelectedItem));
                }
            }
            else
            {
                MessageBox.Show(errorselect);
            }
        }

        private void btnToggleBlokkeren_Click(object sender, EventArgs e)
        {
            if (cbSpoorSectorblokkeren.SelectedItem != null && cbSectorSectorblokkeren.SelectedItem != null)
            {
                int spoornr = Convert.ToInt32(cbSpoorSectorblokkeren.SelectedItem.ToString());
                int sectorid = Convert.ToInt32(cbSectorSectorblokkeren.SelectedItem.ToString());

                Spoor spoor = bc.GetSpoorByNummer(spoornr);
                if (spoor != null)
                {
                    MessageBox.Show(bc.ToggleSector(bc.GetSectorByID(spoor, sectorid)));
                }
                else
                {
                    MessageBox.Show("Spoor kon niet worden getoggled");
                }
            }
            else
            {
                MessageBox.Show("Niet alle vereiste informatie geselecteerd.");
            }
        }

        private void btnBevestigVerwijderen_Click(object sender, EventArgs e)
        {
            if (cbTramVerwijderen.SelectedItem != null)
            {
                int nummer = Convert.ToInt32(cbTramVerwijderen.SelectedItem.ToString());

                MessageBox.Show(bc.TramVerwijderen(nummer));
            }
            else
            {
                MessageBox.Show(errorselect);
            }
        }

        private void btnTramVerplaatsenBevestig_Click(object sender, EventArgs e)
        {
            if (cbTramTramVerplaatsen.SelectedItem != null && cbSectorTramVerplaatsen.SelectedItem != null &&
                cbSpoorTramVerplaatsen.SelectedItem != null)
            {
                int tramnummer = Convert.ToInt32(cbTramTramVerplaatsen.SelectedItem.ToString());
                int spoornummer = Convert.ToInt32(cbSpoorTramVerplaatsen.SelectedItem.ToString());
                int sectornummer = Convert.ToInt32(cbSectorTramVerplaatsen.SelectedItem.ToString());

                Tram tram = bc.GetTramByNummer(tramnummer);
                Spoor spoor = bc.GetSpoorByNummer(spoornummer);
                Sector sector = bc.GetSectorByID(spoor, sectornummer);
                if (tram != null && spoor != null && sector != null)
                {

                    MessageBox.Show(bc.TramVerplaatsen(tram, bc.GetSpoorByTram(tram), bc.GetSectorByTram(tram), spoor,
                        sector));
                }
                else
                {
                    MessageBox.Show("tram, spoor of sector naar zijn null");
                }
            }
            else
            {
                MessageBox.Show("Niet alle vereiste informatie geselecteerd.");
            }
        }

        private void btnSectorReserveren_Click(object sender, EventArgs e)
        {
            if (cbSpoorSectorReserveren.SelectedItem != null && cbSectorSectorReserveren.SelectedItem != null &&
                cbTramSectorReserveren.SelectedItem != null)
            {
                int spoornr = Convert.ToInt32(cbSpoorSectorReserveren.SelectedItem.ToString());
                int sectorid = Convert.ToInt32(cbSectorSectorReserveren.SelectedItem.ToString());
                int tramnr = Convert.ToInt32(cbTramSectorReserveren.SelectedItem.ToString());

                Sector sector = bc.GetSectorByID(bc.GetSpoorByNummer(spoornr), sectorid);
                Tram tram = bc.GetTramByNummer(tramnr);
                if (tram != null && sector != null)
                {
                    MessageBox.Show(bc.SectorReserveren(sector, tram));
                }
                else
                {
                    MessageBox.Show("Sector of tram niet gevonden");
                }

                FillDgvReservering();
            }
            else
            {
                MessageBox.Show(errorselect);
            }
        }

        private void btnSpoorSectorBlokkeren_Click(object sender, EventArgs e)
        {
            if (cbSpoorSectorblokkeren.SelectedItem != null)
            {
                int spoor = Convert.ToInt32(cbSpoorSectorblokkeren.SelectedItem.ToString());

                MessageBox.Show(bc.ToggleSpoor(bc.GetSpoorByNummer(spoor)));
            }
            else
            {
                MessageBox.Show(errorselect);
            }
        }

        private void FillGUIsporen()
        {

            bc.Update();

            int rowCounter = 0;
            int cellCounter = 0;
            int testCounter = 0;

            foreach (Spoor spoor in bc.Sporen)
            {
                foreach (Sector sector in spoor.Sectoren)
                {
                    if (sector.Status == SectorStatus.bezet)
                    {
                        if (sector.Tram != null)
                        {
                            dgvBeheer.Rows[rowCounter].Cells[cellCounter].Value = sector.Tram.TramNummer;
                            rowCounter++;
                            testCounter++;
                        }
                        else
                        {
                            dgvBeheer.Rows[rowCounter].Cells[cellCounter].Value = "";
                            rowCounter++;
                            testCounter++;
                        }
                    }
                    else
                    {
                        dgvBeheer.Rows[rowCounter].Cells[cellCounter].Value = "";
                        rowCounter++;
                        testCounter++;
                    }
                }
                cellCounter++;
                rowCounter = 0;
            }

            int rowCounterIU = 0;
            //int cellCounterIU = 0;
            dgvInEnUitrijsysteem.Rows.Clear();

            foreach (Spoor spoor in bc.Sporen)
            {
                foreach (Sector sector in spoor.Sectoren)
                {
                    if (sector.Status == SectorStatus.bezet && sector.Tram != null)
                    {
                        dgvInEnUitrijsysteem.Rows.Add();
                        dgvInEnUitrijsysteem.Rows[rowCounterIU].Cells[0].Value = sector.Tram.TramNummer; //TODO: nullreference, tram bestaat soms niet meer bij sommige sectoren.
                        dgvInEnUitrijsysteem.Rows[rowCounterIU].Cells[1].Value = spoor.SpoorNummer;
                        dgvInEnUitrijsysteem.Rows[rowCounterIU].Cells[2].Value = sector.Id;
                        dgvInEnUitrijsysteem.Rows[rowCounterIU].Cells[3].Value = sector.Tram.Status;
                        dgvInEnUitrijsysteem.Rows[rowCounterIU].Cells[4].Value = sector.Tram.VertrekTijd;
                        rowCounterIU++;
                    }
                }
            }
        }

        private void cbSpoorSectorblokkeren_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSectorSectorblokkeren.Items.Clear();
            int spoornummer = Convert.ToInt32(cbSpoorSectorblokkeren.SelectedItem.ToString());
            Spoor spoor = bc.GetSpoorByNummer(spoornummer);

            if (spoor == null)
            {
                MessageBox.Show("Kon geen spoor vinden");
                return;
            }

            foreach (Sector s in spoor.Sectoren)
            {
                cbSectorSectorblokkeren.Items.Add(s.Id);
            }
            cbSectorSectorblokkeren.SelectedIndex = 0;
            cbSectorSectorblokkeren.Enabled = true;
        }

        private void cbSpoorSectorReserveren_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSectorSectorReserveren.Items.Clear();
            int spoornummer = Convert.ToInt32(cbSpoorSectorReserveren.SelectedItem.ToString());
            Spoor spoor = bc.GetSpoorByNummer(spoornummer);

            if (spoor == null)
            {
                MessageBox.Show("Kon geen sector vinden");
                return;
            }

            foreach (Sector s in spoor.Sectoren)
            {
                cbSectorSectorReserveren.Items.Add(s.Id);
            }
            cbSectorSectorReserveren.SelectedIndex = 0;
            cbSectorSectorReserveren.Enabled = true;
        }

        private void cbSpoorTramVerplaatsen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSectorTramVerplaatsen.Items.Clear();
            int spoornummer = Convert.ToInt32(cbSpoorTramVerplaatsen.SelectedItem.ToString());

            Spoor spoor = bc.GetSpoorByNummer(spoornummer);

            if (spoor == null)
            {
                MessageBox.Show("Kon geen sector vinden");
                return;
            }

            foreach (Sector s in spoor.Sectoren)
            {
                if (s.Status == SectorStatus.leeg)
                {
                    cbSectorTramVerplaatsen.Items.Add(s.Id);
                }
            }
            cbSectorTramVerplaatsen.SelectedIndex = 0;
            cbSectorTramVerplaatsen.Enabled = true;
        }

        private void FillDgvReservering()
        {
            List<Reservering> reserveringen = bc.GetAllReserveringen();
            dgvReserveringen.Rows.Clear();
            dgvReserveringen.Columns.Add("col1", "Tram Nr");
            dgvReserveringen.Columns.Add("col2", "Sector ID");

            //MessageBox.Show("Reserveringen uit db " + reserveringen.Count);
            for (int i = 0; i < reserveringen.Count; i++)
            {
                dgvReserveringen.Rows.Add(1);
                dgvReserveringen.Rows[i].Cells[0].Value = reserveringen[i].Tram.TramNummer;
                dgvReserveringen.Rows[i].Cells[1].Value = reserveringen[i].Sector.Id;
            }
            //MessageBox.Show("DGV reservering aantal rows " + dgvReserveringen.RowCount);
        }

        #endregion

        #region In-Uitrij 

        private void btnBevestigenTramInEnUitrij_Click(object sender, EventArgs e)
        {
            int tramnummer;
            Int32.TryParse(tbTramnummerInEnUitrij.Text, out tramnummer);
            Tram tram = bc.GetTramByNummer(tramnummer); //TODO: keypress event zodat er alleen maar nummers kunnen worden ingevoerd.
            if (tram != null)
            {
                if (tram.Status == TramStatus.InDienst)
                {
                    ic.DeelTramInBeter(tram);
                }
            }

            /*
            foreach (Tram tram in bc.Trams)
            {
                if (tram.TramNummer == Convert.ToInt32(tbTramnummerInEnUitrij.Text))
                {
                    if (tram.Status == TramStatus.InDienst)
                    {
                MessageBox.Show("Kon de tram met nummer " + tramnr + " niet vinden.");
                        return;
                    }
                }
            }*/
        }

        #endregion

        private void cbTramnummerTramstatusAanpassen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTramnummerTramstatusAanpassen.SelectedItem != null)
            {
                cbTramstatusTramstatusAanpassen.Enabled = true;
                int tramnummer = Convert.ToInt32(cbTramnummerTramstatusAanpassen.SelectedItem.ToString());
                Tram tram = bc.GetTramByNummer(tramnummer);
                cbTramstatusTramstatusAanpassen.SelectedItem = tram.Status;
            }
            else
            {
                cbTramstatusTramstatusAanpassen.Enabled = false;
            }
        }

        private void cbSchoonmaakType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbInUitRijTram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInUitRijTram.SelectedItem != null)
            {
                cbInUitRijTramstatus.Enabled = true;
                int tramnummer = Convert.ToInt32(cbInUitRijTram.SelectedItem.ToString());
                Tram tram = bc.GetTramByNummer(tramnummer);
                cbInUitRijTramstatus.SelectedItem = tram.Status;
            }
            else
            {
                cbInUitRijTramstatus.Enabled = false;
            }
        }

        private void btnBevestigenTramstatus_Click(object sender, EventArgs e)
        {
            if (cbInUitRijTram.SelectedItem != null &&
                cbInUitRijTramstatus.SelectedItem != null)
            {
                Tram tram = bc.GetTramByNummer(Convert.ToInt32(cbInUitRijTram.SelectedItem.ToString()));
                if (tram.Status != (TramStatus)cbInUitRijTramstatus.SelectedItem)
                {
                    MessageBox.Show(bc.TramStatusAanpassen(tram,
                        (TramStatus)cbInUitRijTramstatus.SelectedItem));
                }
                else
                {
                    MessageBox.Show("Deze tram heeft al de geselecteerde status.");
                }
            }
        }

        private void btnVerwijderReservering_Click(object sender, EventArgs e)
        {
            foreach (Reservering reservering in bc.GetAllReserveringen())
            {
                if(reservering.ToString() == cbVerwijderReservering.SelectedItem.ToString())
                {
                    //bc.VerwijderReservering(reservering);
                }
            }
        }

        private void btnStopSimulatie_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}