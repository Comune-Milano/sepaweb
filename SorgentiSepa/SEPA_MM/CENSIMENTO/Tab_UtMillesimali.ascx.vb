
Partial Class CENSIMENTO_Tab_Millesimali
    Inherits UserControlSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Private Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                vId = CType(Me.Page, Object).vId
                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If
                If Session("SLE") = "1" Then
                    FrmSolaLettura()
                End If

            End If
            '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
            If vId = 0 Then
                vId = CType(Me.Page, Object).vId
            End If
            '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************
            cerca()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.BtnElimina.Visible = False
            Me.BtnADD.Visible = False
            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Enabled = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub cerca()
        Try

            Select Case Me.Page.Title
                Case "Inserimento Complessi"
                    Passato = "CO"
                    sStringaSql = " SELECT  DISTINCT utenze.id, utenze.cod_tipologia,  ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE  FROM SISCOM_MI.ANAGRAFICA_FORNITORI, SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID  AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE AND TABELLE_MILLESIMALI.ID_COMPLESSO = " & vId & ""

                    'sStringaSql = " SELECT  DISTINCT utenze.id, utenze.cod_tipologia, utenze.fornitore, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE  FROM SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID  AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND TABELLE_MILLESIMALI.ID_COMPLESSO = " & vId & ""
                    QUERY = sStringaSql
                    BindGrid()
                Case "Inserimento EDIFICI"
                    Passato = "ED"
                    sStringaSql = " SELECT DISTINCT utenze.id, utenze.cod_tipologia, ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE FROM SISCOM_MI.ANAGRAFICA_FORNITORI,SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID  AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE AND TABELLE_MILLESIMALI.ID_EDIFICIO = " & vId & " "

                    'sStringaSql = " SELECT DISTINCT utenze.id, utenze.cod_tipologia, utenze.fornitore, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID  AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND TABELLE_MILLESIMALI.ID_EDIFICIO = " & vId & " "
                    QUERY = sStringaSql
                    BindGrid()
            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub
    Private Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property
    Private Sub BindGrid()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "INTERV_ADEG_NORM")
            DatGridUtenzaMillesim.DataSource = ds
            DatGridUtenzaMillesim.DataBind()




        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub DatGridUtenzaMillesim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DatGridUtenzaMillesim.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_UtMillesimali1_txtmia').value='Hai selezionato il contratto " & e.Item.Cells(1).Text & "';document.getElementById('Tab_UtMillesimali1_HFtxtId').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_UtMillesimali1_HFtxtDesc').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_UtMillesimali1_txtmia').value='Hai selezionato il contratto " & e.Item.Cells(1).Text & "';document.getElementById('Tab_UtMillesimali1_HFtxtId').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_UtMillesimali1_HFtxtDesc').value='" & e.Item.Cells(2).Text & "'")

        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If HFtxtId.Value <> "Label" And HFtxtId.Value <> "" Then
            Dim IDUTENZA As Integer
            IDUTENZA = HFtxtId.Value
            Response.Write("<script>window.open('UtenzeMillesimali.aspx?ID=" & vId & ",&IDUTENZA=" & IDUTENZA & ",&Pas=" & Passato & "&APERTURA=UPDATE','DETTUTMILLES', 'resizable=no, width=630, height=500');</script>")
        Else
            Response.Write("<SCRIPT>alert('Selezionare una utenza!');</SCRIPT>")

        End If
    End Sub
    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        If EsistonoMilles() Then
            Response.Write("<script>window.open('UtenzeMillesimali.aspx?ID=" & vId & ",&Pas=" & Passato & "','DETTUTMILLES', 'resizable=no, width=630, height=500');</script>")

        Else

            Exit Sub
        End If


    End Sub
    Private Function EsistonoMilles() As Boolean
        'Richiamo la connessione
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        Dim trovatiMillesimali As Boolean = False
        'Richiamo la transazione
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans
        Select Case Me.Page.Title
            Case "Inserimento Complessi"
                par.cmd.CommandText = "select * from siscom_mi.tabelle_millesimali where Id_complesso = " & vId

            Case "Inserimento EDIFICI"
                par.cmd.CommandText = "select * from siscom_mi.tabelle_millesimali where id_edificio = " & vId

        End Select

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            EsistonoMilles = True
        Else
            Response.Write("<script>alert('Non esistono TABELLE MILLESIMALI da associare ad una UTENZA MILLESIMALE!');</script>")

            EsistonoMilles = False
        End If
        Return EsistonoMilles
    End Function


    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        Try
            If HFtxtId.Value <> "Label" And Me.HFtxtId.Value <> "" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "delete from SISCOM_MI.utenze where id = " & HFtxtId.Value
                par.cmd.ExecuteNonQuery()
                Select Case Passato
                    Case "CO"

                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_COMPLESSI(ID_COMPLESSO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F56','UTENZE MILLESIMALI')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************
                    Case "ED"
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F56','UTENZE MILLESIMALI')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************

                End Select
                Session.Item("MODIFICASOTTOFORM") = 1
                CType(Me.Page, Object).VerificaModificheSottoform()

                Me.txtmia.Text = ""
                HFtxtId.Value = ""
                BindGrid()
            Else
                Response.Write("<SCRIPT>alert('Selezionare una utenza!');</SCRIPT>")

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub
End Class
