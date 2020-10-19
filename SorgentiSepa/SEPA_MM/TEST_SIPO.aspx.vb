Imports System.ServiceModel.Channels
Imports System.ServiceModel
Imports System.Net
Imports System.IO

Partial Class TEST_SIPO
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TestoFamiglia As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim TestoErrore As String = ""
        Dim SIPO_APPLICAZIONE As String = ""
        Dim SIPO_OPERATORE As String = ""
        Dim SIPO_PWOPERATORE As String = ""
        Dim SIPO_TOKEN As String = ""
        Dim i As Integer = 1
        Dim NUMERO As String = ""
        Dim DATARILASCIO As String = ""
        Dim DATASCADENZA As String = ""
        Dim DATAPROROGA As String = ""
        Dim COMUNE As String = ""

        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=53"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            SIPO_APPLICAZIONE = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=54"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            SIPO_OPERATORE = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=55"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            SIPO_PWOPERATORE = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=56"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            SIPO_TOKEN = par.IfNull(myReader1("VALORE"), "")
        End If
        myReader1.Close()

        Dim obj As New Anagrafe11.XMLWSAnagrafe2009SoapClient
        Dim risultato As Anagrafe11.getRicercaIndividuiXMLResult
        Dim httpRequestProperty As New HttpRequestMessageProperty

      
        Try
           
            httpRequestProperty.Method = "POST"
            httpRequestProperty.Headers.Add("Authorization", "Bearer " & SIPO_TOKEN)
            Using scope As New OperationContextScope(obj.InnerChannel)
                OperationContext.Current.OutgoingMessageProperties(HttpRequestMessageProperty.Name) = httpRequestProperty
                risultato = obj.getRicercaIndividuiXML(SIPO_APPLICAZIONE, SIPO_OPERATORE, SIPO_PWOPERATORE, Session.Item("ID_OPERATORE"), "", "", "", "", "", "", 0, "", "BRNLRI78S69F205V", 0, "", "N", "T")
            End Using
            Label1.Text = "ok " & risultato.Item(0).Matricola
        Catch FaultException As System.ServiceModel.FaultException
            Dim FaultDetail As System.Xml.XmlDictionaryReader
            FaultDetail = FaultException.CreateMessageFault.GetReaderAtDetailContents
            Dim errorcode = FaultDetail.ReadElementString
            Dim errormessage = FaultDetail.ReadElementString
            Label1.Text = "Trovato: " & errorcode & ":" & errormessage

        Catch ex As Exception
            Label1.Text = "Non rilevato:" & ex.Message
        End Try

    End Sub
End Class
