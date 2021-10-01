using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = iTextSharp.text.Rectangle;

namespace crearPDF
{
    public partial class Form1 : Form
    {
        string pathPDF = @"C:\Users\unive\Desktop\FITEC\sistemasoperativos\PDF\original.pdf";
        string pathPDF2 = @"C:\Users\unive\Desktop\FITEC\sistemasoperativos\PDF\con_texto.pdf";


        public Form1()
        {
            InitializeComponent();
        }
        string Texto;
        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Texto = richTextBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Objeto para leer el pdf original
            PdfReader oReader = new PdfReader(pathPDF);
            //Objeto que tiene el tamaño de nuestro documento
            Rectangle oSize = oReader.GetPageSizeWithRotation(1);
            //documento de itextsharp para realizar el trabajo asignandole el tamaño del original
            Document oDocument = new Document(oSize);

            // Creamos el objeto en el cual haremos la inserción
            FileStream oFS = new FileStream(pathPDF2, FileMode.Create, FileAccess.Write);
            PdfWriter oWriter = PdfWriter.GetInstance(oDocument, oFS);
            oDocument.Open();

            //El contenido del pdf, aqui se hace la escritura del contenido
            PdfContentByte oPDF = oWriter.DirectContent;

            //Propiedades de nuestra fuente a insertar
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            oPDF.SetColorFill(BaseColor.BLACK);
            oPDF.SetFontAndSize(bf, 8);

            //Se abre el flujo para escribir el texto
            oPDF.BeginText();
            string text = Texto;
            // Le damos posición y rotación al texto
            // la posición de Y es al revés de como estamos acostumbrados
            oPDF.ShowTextAligned(2, text, 100, oSize.Height - 10, 0);
            oPDF.EndText();

            //crea una nueva pagina y agrega el pdf original
            PdfImportedPage page = oWriter.GetImportedPage(oReader, 1);
            oPDF.AddTemplate(page, 0, 0);

            // Cerramos los objetos utilizados
            oDocument.Close();
            oFS.Close();
            oWriter.Close();
            oReader.Close();
            //mensaje de guardado
            MessageBox.Show("Se guardó exitosamente");
        }
    }
}
