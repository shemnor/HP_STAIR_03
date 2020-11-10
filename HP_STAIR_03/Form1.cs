using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Drawing.UI;
using TSDrg = Tekla.Structures.Drawing;
using TSG = Tekla.Structures.Geometry3d;

namespace HP_STAIR_03
{
    public partial class Form1 : Form
    {
        bool invalidCell = false;
        public Form1()
        {
            InitializeComponent();
            this.dgv_dimTable.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            this.dgv_dimTable.CellEndEdit += new DataGridViewCellEventHandler(dgv_dimTable_CellEndEdit);
            this.dgv_dimTable.RowValidating += new DataGridViewCellCancelEventHandler(dgv_dimTable_RowValidating);
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = dgv_dimTable.Columns[e.ColumnIndex].HeaderText;
            string headerName = dgv_dimTable.Columns[e.ColumnIndex].Name;
            int validInt;
            double validDouble;
            invalidCell = false;
            bool filledRow = false;

            foreach (DataGridViewCell cell in dgv_dimTable.Rows[e.RowIndex].Cells)
            {
                if (!(string.IsNullOrEmpty(cell.EditedFormattedValue.ToString())))
                {
                    filledRow = true;
                    break;
                }
            }

            //if its not the last row used for adding rows
            if(e.RowIndex != dgv_dimTable.Rows.Count - 1)
            {
                //if the row is empty
                if (!filledRow)
                {
                    BeginInvoke(new MethodInvoker(delegate { dgv_dimTable.Rows.RemoveAt(e.RowIndex); }));
                }
                else
                {
                    switch (headerName)
                    {
                        case ("quantBars"):
                        case ("spacingBars"):
                            if (!int.TryParse(e.FormattedValue.ToString(), out validInt) && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                            {
                                dgv_dimTable.Rows[e.RowIndex].ErrorText = $"Value given for {headerText} should be an Integer";
                                invalidCell = true;
                            }
                            break;
                        case ("dimLength"):
                            if (!(double.TryParse(e.FormattedValue.ToString(), out validDouble)))
                            {
                                dgv_dimTable.Rows[e.RowIndex].ErrorText = $"Value given for {headerText} should be an Integer or decimal";
                                invalidCell = true;
                            }
                            else if (e.FormattedValue == null)
                            {
                                dgv_dimTable.Rows[e.RowIndex].ErrorText = $"You must enter a value in the {headerText} column";
                                invalidCell = true;
                            }
                            break;
                    }
                }
            }

        }

        private void dgv_dimTable_RowValidating(Object sender, DataGridViewCellCancelEventArgs e)
        {
            foreach (DataGridViewCell cell in dgv_dimTable.Rows[e.RowIndex].Cells)
            {
                string headerText = dgv_dimTable.Columns[cell.ColumnIndex].HeaderText;
                string headerName = dgv_dimTable.Columns[cell.ColumnIndex].Name;

                switch (headerName)
                {
                    case ("quantBars"):
                        if (!string.IsNullOrEmpty(cell.FormattedValue.ToString()) && string.IsNullOrEmpty(dgv_dimTable.Rows[cell.RowIndex].Cells["spacingBars"].FormattedValue.ToString()))
                        {
                            dgv_dimTable.Rows[cell.RowIndex].ErrorText = $"Value for spacing is missing";
                        }
                        break;
                    case ("spacingBars"):
                        if (!string.IsNullOrEmpty(cell.FormattedValue.ToString()) && string.IsNullOrEmpty(dgv_dimTable.Rows[cell.RowIndex].Cells["quantBars"].FormattedValue.ToString()))
                        {
                            dgv_dimTable.Rows[cell.RowIndex].ErrorText = $"Value for Number of Bars is missing";
                        }
                        break;
                }
            }
        }

        void dgv_dimTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!invalidCell)
            {
                // Clear the row error in case the user presses ESC.
                dgv_dimTable.Rows[e.RowIndex].ErrorText = String.Empty;
            }
            

        }

        private void resetFormStatus()
        {
            lbl_status.Text = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //reset dialog
            resetFormStatus();
            
            //get insertion point inpit from user
            Tuple<TSG.Point, TSDrg.View> usrInp = GetPointViewFromUser();
            TSG.Point basePoint = usrInp.Item1;
            TSG.Point endPoint;
            TSDrg.View currentView =usrInp.Item2;

            if (basePoint == null)
            {
                return;
            }

            //create dimension
            foreach (DataGridViewRow row in dgv_dimTable.Rows)
            {
                if (row.Index < dgv_dimTable.Rows.Count-1)
                {
                    var dimLengthInp = row.Cells["dimLength"].Value;
                    double dimLength = Convert.ToDouble(dimLengthInp.ToString());

                    if (checkBox1.Checked)
                    {
                        endPoint = new TSG.Point(basePoint.X, basePoint.Y + dimLength);
                    }
                    else if (checkBox2.Checked)
                    {
                        endPoint = new TSG.Point(basePoint.X+dimLength, basePoint.Y);
                    }
                    else
                    {
                        lbl_status.Text = "Please select dimension orientation";
                        return;
                    }


                    TSDrg.PointList pointList = new TSDrg.PointList();
                    pointList.Add(basePoint);
                    pointList.Add(endPoint);

                    TSDrg.StraightDimensionSet newDimSet = CreateNewDim(currentView, pointList);
                    newDimSet.Select();
                    newDimSet.Attributes.LoadAttributes(txb_preset.Text);
                    newDimSet.Modify();
                    basePoint = endPoint;
                }
            }
        }

        private void modifyDim(TSDrg.StraightDimension line)
        {
            line.Select();
            TSDrg.StraightDimensionSet sds = line.GetDimensionSet() as TSDrg.StraightDimensionSet;

            //TSDrg.FontAttributes fa = sds.Attributes.Text.Font;
            //TSDrg.FontAttributes oldFa = new TSDrg.FontAttributes(fa.Color, fa.Height, fa.Name, fa.Italic, fa.Bold);
            line.Attributes.MiddleLowerTag.Add(new TSDrg.TextElement("ya fud"));
            line.Modify();
            //sds.Select();
            //sds.Attributes.Text.Font = oldFa;
            //sds.Modify();
        }
        private void moveDim(TSDrg.StraightDimension line, TSG.Point startPoint, TSG.Point endPoint)
        {
            line.Select();
            line.StartPoint = startPoint;
            line.EndPoint = endPoint;
            line.Modify();
        }

        private TSDrg.StraightDimensionSet CreateNewDim(TSDrg.View view, TSDrg.PointList pointList)
        {
            TSG.Vector upDirection;

            if (checkBox1.Checked)
            {
                upDirection = new TSG.Vector(1, 0, 0);
            }
            else if (checkBox2.Checked)
            {
                upDirection = new TSG.Vector(0, 1, 0);
            }
            else
            {
                lbl_status.Text = "Please select dimension orientation";
                upDirection = new TSG.Vector(1, 0, 0);
            }

            double height = 0.0;
            TSDrg.StraightDimensionSet.StraightDimensionSetAttributes attr = new TSDrg.StraightDimensionSet.StraightDimensionSetAttributes();

            TSDrg.StraightDimensionSetHandler myDimSetHandler = new TSDrg.StraightDimensionSetHandler();
            TSDrg.StraightDimensionSet newDim = myDimSetHandler.CreateDimensionSet(view, pointList, upDirection, height, attr);
            return newDim;
        }

        private Tuple<TSG.Point, TSDrg.View> GetPointViewFromUser()
        {
            TSDrg.DrawingHandler myDrawingHandler = new TSDrg.DrawingHandler();
            TSDrg.UI.Picker pointPicker = myDrawingHandler.GetPicker();
            TSG.Point myPoint = null;
            TSDrg.ViewBase myViewBase = null;
            TSDrg.View myView = myViewBase as TSDrg.View;

            try 
            {
                pointPicker.PickPoint("Pick a point to insert dimesnion", out myPoint, out myViewBase);
                myView = myViewBase as TSDrg.View;

                while (myView == null)
                {
                    pointPicker.PickPoint("Selected point is not inside a view. Pick a point to insert dimension", out myPoint, out myViewBase);
                    myView = myViewBase as TSDrg.View;
                }

                return new Tuple<TSG.Point, TSDrg.View>(myPoint, myView);
            }

            catch (Tekla.Structures.Drawing.PickerInterruptedException interrupted)
            {
                //Tekla.Structures.Model.Operations.Operation.DisplayPrompt("THIS METHOD NOT WORKING BECAUSE TEKLA API IS THE WORST THING I HAVE EVER WORKED WITH");
                lbl_status.Text = "User interrupted action.";
                return new Tuple<TSG.Point, TSDrg.View>(myPoint, myView);
            }
        }

        private void binbin()
        {
            /*
            var picker = new DrawingHandler().GetPicker();
            //List<DrawingObjectEnumerator> related = new List<DrawingObjectEnumerator>();
            List<DrawingObject> container = new List<DrawingObject>();
            DrawingHandler drawingHandler = new DrawingHandler();
            DrawingObjectSelector selector = drawingHandler.GetDrawingObjectSelector();
            int x = 1;

            var objects = selector.GetSelected();
            while (objects.MoveNext())
            {
                container.Add(objects.Current);
            }

            foreach (StraightDimensionSet objecto in container)
            {
                objecto.Distance = 1;
                //bool isOK1 = objecto.Attributes.LoadAttributes("HP_STAIR_DIM_LEFT");
                //bool isOK2 = objecto.Attributes.LoadAttributes("HP_STAIR_DIM_LINE_COMB");
                objecto.Modify();
                Console.ReadKey();
            }
            
            }

            private void button2_Click(object sender, EventArgs e)
        {
            //reset dialog
            resetFormStatus();


            //get insertion point inpit from user
            Tuple<TSG.Point, TSDrg.View> usrInp = GetPointViewFromUser();
            TSG.Point basePoint = usrInp.Item1;
            TSDrg.View currentView = usrInp.Item2;
            
            System.Type[] Types = new System.Type[1];
            Types.SetValue(typeof(TSDrg.StraightDimension), 0);
            TSDrg.DrawingObjectEnumerator allDimLines = currentView.GetAllObjects(Types);

            foreach (TSDrg.StraightDimension line in allDimLines)
            {
                addDimPrefix(line);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //reset dialog
            resetFormStatus();

            TSDrg.DrawingHandler myDrawingHandler = new TSDrg.DrawingHandler();
            TSDrg.Drawing CurrentDrawing = myDrawingHandler.GetActiveDrawing();

            //get insertion point inpit from user
            Tuple<TSG.Point, TSDrg.View> usrInp = GetPointViewFromUser();
            TSG.Point basePoint = usrInp.Item1;
            TSDrg.View currentView = usrInp.Item2;
            TSG.Point endPoint = new TSG.Point(basePoint.X, basePoint.Y + 100.0);

            TSDrg.PointList PointList = new TSDrg.PointList();
            PointList.Add(basePoint);
            PointList.Add(endPoint);
            TSG.Vector upDirection = new TSG.Vector(1, 0, 0);
            double height = 0.0;
            TSDrg.StraightDimensionSet.StraightDimensionSetAttributes attr = new TSDrg.StraightDimensionSet.StraightDimensionSetAttributes();

            TSDrg.StraightDimensionSetHandler myDimSetHandler = new TSDrg.StraightDimensionSetHandler();
            TSDrg.StraightDimensionSet XDimensions = myDimSetHandler.CreateDimensionSet(currentView, PointList, upDirection, height, attr);
            
            CurrentDrawing.CommitChanges();
            */
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }
    }
    

}
