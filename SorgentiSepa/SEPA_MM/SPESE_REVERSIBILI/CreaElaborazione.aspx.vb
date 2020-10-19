
Partial Class SPESE_REVERSIBILI_CreaElaborazione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        'IMPOSTAZIONE DEL TITOLO DELLA PAGINA
        CType(Master.FindControl("TitoloMaster"), Label).Text = "Creazione nuova elaborazione"
        If Not IsPostBack Then
            caricaPiano()
        End If
    End Sub
    Protected Sub ButtonCreaElaborazione_Click(sender As Object, e As System.EventArgs) Handles ButtonCreaElaborazione.Click
        Try
            connData.apri()
            'CREO ID NUOVA ELABORAZIONE
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ELABORAZIONE_SR.NEXTVAL FROM DUAL"
            Dim idElaborazione As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            'CREO NUOVA ELABORAZIONE CON NOTA
            Dim piano As String = "NULL"
            Dim dataCI As String = "NULL"
            Dim dataCF As String = "NULL"
            Dim dataPI As String = "NULL"
            Dim dataPF As String = "NULL"
            If IsNumeric(cmbPiano.SelectedValue) AndAlso CInt(cmbPiano.SelectedValue) <> -1 Then
                piano = cmbPiano.SelectedValue
                par.cmd.CommandText = "SELECT SISCOM_MI.GETDATA (INIZIO) AS INIZIO,SISCOM_MI.GETDATA (FINE) AS FINE FROM SISCOM_MI.PF_MAIN, SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND PF_MAIN.ID=" & cmbPiano.SelectedValue
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    dataCI = par.IfNull(lettore("INIZIO"), "")
                    dataCF = par.IfNull(lettore("FINE"), "")
                    dataPI = par.IfNull(lettore("INIZIO"), "")
                    dataPF = par.IfNull(lettore("FINE"), "")
                End If
                lettore.Close()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI(ID,NOTE,ID_PIANO_FINANZIARIO,DATA_RIFERIMENTO_INIZIO_P,DATA_RIFERIMENTO_FINE_P,DATA_RIFERIMENTO_INIZIO_C,DATA_RIFERIMENTO_FINE_C)" _
                    & "VALUES (" & idElaborazione & ",'" & par.PulisciStrSql(TextBoxNoteElaborazione.Text) & "'," & piano & ",'" & dataPI & "','" & dataPF & "','" & dataCI & "','" & dataCF & "')"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI(ID,NOTE,ID_PIANO_FINANZIARIO)" _
                    & "VALUES (" & idElaborazione & ",'" & par.PulisciStrSql(TextBoxNoteElaborazione.Text) & "',NULL)"
                par.cmd.ExecuteNonQuery()
            End If
            connData.chiudi()
            'RICARICO LA PAGINA CON TUTTE LE ELABORAZIONI
            RadWindowManager1.RadAlert("Elaborazione creata correttamente!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " ButtonCreaElaborazione_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante la creazione dell\'elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub caricaPiano()
        Try
            connData.apri()
            par.caricaComboTelerik("SELECT PF_MAIN.ID, SISCOM_MI.GETDATA (INIZIO) || '-' || SISCOM_MI.GETDATA (FINE) AS DESCRIZIONE FROM SISCOM_MI.PF_MAIN, SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID ORDER BY ID DESC", cmbPiano, "ID", "DESCRIZIONE", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Crea Elaborazione - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante la creazione dell\'elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
End Class
