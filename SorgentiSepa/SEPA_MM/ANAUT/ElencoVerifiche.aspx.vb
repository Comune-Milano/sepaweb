
Partial Class ANAUT_ElencoVerifiche
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label6.Text = Label6.Text & Format(Now, "dd/MM/yyyy")
            Ordinamento = "UTENZA_COMP_NUCLEO.COGNOME ASC,UTENZA_COMP_NUCLEO.NOME ASC"
            Carica()


        End If
    End Sub

    Private Property Ordinamento() As String
        Get
            If Not (ViewState("par_Ordinamento") Is Nothing) Then
                Return CStr(ViewState("par_Ordinamento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Ordinamento") = value
        End Set

    End Property

    Private Function Carica()

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        '********CONNESSIONE*********
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Dim Indice As String = ""

        par.cmd.CommandText = "select * from utenza_bandi where stato=1 order by id desc"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Label6.Text = Label6.Text & " - BANDO " & par.IfNull(myReader("descrizione"), "")
            Indice = par.IfNull(myReader("id"), "0")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT COUNT(ID) FROM UTENZA_DICHIARAZIONI WHERE id_bando=" & Indice & " AND fl_da_verificare='1' AND POSIZIONE IS NULL"
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            If myReader(0) > 0 Then
                lblContatore.Text = lblContatore.Text & " Trovate : " & myReader(0) & " dichiarazioni DA VERIFICARE non legate a nessun rapporto (Cod.Convocazione non trovato)"
            End If
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_COMP_NUCLEO.cognome,UTENZA_COMP_NUCLEO.nome,COMUNI_NAZIONI.NOME AS COMUNE,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_INDIRIZZO," _
                            & "TAB_FILIALI.NOME AS FILIALE,GetOperatoreAU(UTENZA_DICHIARAZIONI.ID) AS OPERATORE,DECODE(NVL(FL_VERIFICA_REDDITO,0),0,'NO',1,'SI') AS REDDITI,DECODE(NVL(FL_VERIFICA_NUCLEO,0),0,'NO',1,'SI') AS NUCLEO,DECODE(NVL(FL_VERIFICA_PATRIMONIO,0),0,'NO',1,'SI') AS PATRIMONIO FROM UTENZA_DICHIARAZIONI, UTENZA_COMP_NUCLEO, COMUNI_NAZIONI, T_TIPO_INDIRIZZO, SISCOM_MI.TAB_FILIALI, SISCOM_MI.UNITA_IMMOBILIARI, " _
                            & "SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                            & "WHERE utenza_dichiarazioni.id_bando=" & Indice & " and COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND UTENZA_DICHIARAZIONI.POSIZIONE=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND " _
                            & "EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND T_TIPO_INDIRIZZO.COD=UTENZA_DICHIARAZIONI.ID_TIPO_IND_RES_DNTE (+) " _
                            & "AND COMUNI_NAZIONI.ID=UTENZA_DICHIARAZIONI.ID_LUOGO_RES_DNTE AND UTENZA_DICHIARAZIONI.fl_da_verificare='1' AND UTENZA_COMP_NUCLEO.id_dichiarazione=UTENZA_DICHIARAZIONI.ID " _
                            & "AND UTENZA_COMP_NUCLEO.progr=UTENZA_DICHIARAZIONI.progr_dnte ORDER BY " & Ordinamento

        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            DataGridRateEmesse.DataSource = dt
            DataGridRateEmesse.DataBind()
        Else
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")
            Response.Write("<script language='javascript'> { self.close() }</script>")
        End If

        lblContatore.Text = lblContatore.Text & " Estratte : " & DataGridRateEmesse.Items.Count





        HttpContext.Current.Session.Add("AA", dt)
        imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=0','Distinta','');")

    End Function

    Protected Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Ordinamento = "UTENZA_DICHIARAZIONI.PG ASC"
        Carica()
    End Sub

    Protected Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Ordinamento = "UTENZA_COMP_NUCLEO.COGNOME ASC,UTENZA_COMP_NUCLEO.NOME ASC"
        Carica()
    End Sub

    Protected Sub RadioButton3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Ordinamento = "TIPO_INDIRIZZO asc,UTENZA_dichiarazioni.IND_RES_DNTE ASC"
        Carica()
    End Sub
End Class
