
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VariazioniNP
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataConsumo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtPercVarCanone.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariaz(this);")
            Me.txtPercVarCons.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariaz(this);")
            'txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            'txtDataConsumo.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            CaricaImpServiziCanone()
            CaricaImpServiziConsumo()

            CaricaVarServizi()
            CaricaVarCons()

            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSoloLettura()
            End If



        End If
        ContSpalmabilità()

    End Sub
    Private Sub FrmSoloLettura()
        Me.btnEliminaServCons.Visible = False
        Me.btnEliminaServ.Visible = False

    End Sub
    Private Sub ContSpalmabilità()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT DISTINCT IVA_CANONE FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim DT As New Data.DataTable
            da.Fill(DT)
            If DT.Rows.Count = 1 Then
                Spalm_Canone.Value = 1
            Else
                Spalm_Canone.Value = 0
                ConfermaSp_Consumo.Value = 0
            End If
            DT.Clear()
            da.Dispose()

            par.cmd.CommandText = "SELECT DISTINCT IVA_CONSUMO FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            DT = New Data.DataTable
            da.Fill(DT)

            If DT.Rows.Count = 1 Then
                Spalm_Consumo.Value = 1
            Else
                Spalm_Consumo.Value = 0
                ConfermaSp_Consumo.Value = 0
            End If
            DT.Clear()
            da.Dispose()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"
        End Try
    End Sub
    Private Sub CaricaVarServizi()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_VARIAZIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_VARIAZIONE,PROVVEDIMENTO,TRIM(TO_CHAR(PERCENTUALE,'9G999G999G999G999G990D99')) AS PERCENTUALE,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 1"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_VARIAZIONI")

            Me.DataGridVariazServ1.DataSource = ds
            DataGridVariazServ1.DataBind()


            Dim tot As Double = 0
            par.cmd.CommandText = "SELECT SUM(NVL(percentuale,0)) as PERCVAR FROM siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " AND id_tipologia = 1 AND appalti_variazioni.ID = appalti_variazioni_importi.id_variazione GROUP BY id_variazione"
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            For Each row As Data.DataRow In dt.Rows
                tot = tot + (par.IfNull(row.Item("PERCVAR"), 0) / Me.DataGridImpVariazCanone.Items.Count)
            Next
            Me.PercUsataCanone.Value = tot



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub

    Protected Sub btn_InserisciAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciAppalti.Click
        salvaServCanone()

    End Sub
    Private Sub salvaServCanone()
        Try
            If par.IfEmpty(Me.txtData.Text, "") <> "" Then
                Dim i As Integer = 0



                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                'CONTROLLO SUI DATI INSERITI IN MODO CHE NON SUPERINO IL 20% previsto dalla legge
                If Me.ConfermaSp_Canone.Value = 1 Then
                    If CDbl(PercUsataCanone.Value) + CDbl(txtPercVarCanone.Text) > 20 Then
                        Response.Write("<SCRIPT>alert('Attenzione...L\'importo percentuale di variazione, supera quello massimo previsto dalla legge (20%)!\nImporto percentuale di variazione residuo pari a: " & Format((20 - CDbl(PercUsataCanone.Value)), "##,##0.00") & "%');</SCRIPT>")
                        Response.Write("<SCRIPT>alert('Nessuna Variazione è stata memorizzata!');</SCRIPT>")
                        txtAppareV.Value = "0"
                        Exit Sub
                    End If
                Else
                    Dim DaSommare As Double = 0
                    i = 0
                    For i = 0 To Me.DataGridImpVariazCanone.Items.Count - 1
                        DaSommare = DaSommare + par.IfEmpty((DirectCast(DataGridImpVariazCanone.Items(i).Cells(4).FindControl("txtPercVarCanone"), TextBox).Text), 0)
                    Next
                    DaSommare = DaSommare / DataGridImpVariazCanone.Items.Count
                    If CDbl(PercUsataCanone.Value) + DaSommare > 20 Then
                        Response.Write("<SCRIPT>alert('Attenzione...L\'importo percentuale di variazione, supera quello massimo previsto dalla legge (20%)!');</SCRIPT>")
                        txtAppareV.Value = "0"
                        Exit Sub

                    End If
                End If



                '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.Text) & " AND ID_TIPOLOGIA = 1"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<SCRIPT>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</SCRIPT>")
                    myReader.Close()
                    txtAppareV.Value = "0"
                    Exit Sub
                End If

                Dim IdAppVariazione As Integer = 0
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    IdAppVariazione = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                    & " VALUES(" & IdAppVariazione & ", " & CType(Me.Page, Object).vIdAppalti & ",'" & par.AggiustaData(Me.txtData.Text) & "',1,'" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "')"
                par.cmd.ExecuteNonQuery()

                Dim ImpContSenzaIva As Double = 0
                par.cmd.CommandText = "SELECT SUM ((Importo_canone - (Importo_canone*(sconto_canone/100))))  AS ImpContCanone FROM  siscom_mi.APPALTI_VOCI_PF WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    ImpContSenzaIva = myReader("ImpContCanone")
                End If
                myReader.Close()


                '****************INIZIO A SALVARE LA VARIAZIONE O LE VARIAZIONI
                i = 0
                Dim di As DataGridItem
                Dim idServizio As Integer = 0
                Dim IdVoceServ As Integer = 0
                Dim Importo As Double = 0

                If Me.ConfermaSp_Canone.Value = 1 Then
                    '*********************CASO DELLA SPALMABILITA' AUTOMATICA DA PARTE DEL SISTEMA *************
                    If par.IfEmpty(Me.txtPercVarCanone.Text, "") <> "" Then
                        Importo = (ImpContSenzaIva * (Me.txtPercVarCanone.Text)) / 100
                        Importo = Importo / Me.DataGridImpVariazCanone.Items.Count

                        For i = 0 To Me.DataGridImpVariazCanone.Items.Count - 1
                            di = Me.DataGridImpVariazCanone.Items(i)
                            'idServizio = di.Cells(0).Text
                            IdVoceServ = di.Cells(1).Text
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                & ", " & IdVoceServ & "," & par.VirgoleInPunti(Importo) & "," & par.VirgoleInPunti(par.IfEmpty(Me.txtPercVarCanone.Text, 0)) & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                    Else
                        Response.Write("<SCRIPT>alert('I campi data ed importo sono obbligatori!');</SCRIPT>")
                        txtAppareV.Value = "0"
                    End If
                Else

                    '*********************CASO DEL CICLO PER SALVARE LA VARIAZIONE SU ONGI SINGOLA VOCE ******************
                    For i = 0 To DataGridImpVariazCanone.Items.Count - 1
                        di = Me.DataGridImpVariazCanone.Items(i)
                        'idServizio = di.Cells(0).Text
                        IdVoceServ = di.Cells(1).Text
                        Importo = (par.IfEmpty(di.Cells(6).Text, 0) * par.IfEmpty((DirectCast(di.Cells(4).FindControl("txtPercVarCanone"), TextBox).Text), 0)) / 100
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                        & ", " & IdVoceServ & "," & par.VirgoleInPunti(Importo) & "," & par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVarCanone"), TextBox).Text, 0)) & ")"
                        par.cmd.ExecuteNonQuery()
                    Next

                End If

                Me.txtData.Text = ""
                Me.txtPercVarCanone.Text = ""
                Me.txtNote.Text = ""
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                CaricaVarServizi()
                'End If
            Else
                Response.Write("<SCRIPT>alert('I campi data ed importo sono obbligatori!');</SCRIPT>")
                txtAppareV.Value = "0"
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"


        End Try
    End Sub

    Protected Sub btn_ChiudiAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiAppalti.Click
        Me.txtPercVarCanone.Text = ""
        Me.txtData.Text = ""
        Me.txtNote.Text = ""
        txtAppareV.Value = "0"
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To DataGridImpVariazCanone.Items.Count - 1
            di = DataGridImpVariazCanone.Items(i)
            DirectCast(di.Cells(4).FindControl("txtPercVarCanone"), TextBox).Text = ""
        Next

    End Sub

    Protected Sub DataGridVariazServ1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVariazServ1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Variazioni1_txtmia').value='Hai selezionato la variazione sul SERVIZIO a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_Variazioni1_id_selected').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Variazioni1_txtmia').value='Hai selezionato la variazione sul SERVIZIO a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_Variazioni1_id_selected').value='" & e.Item.Cells(0).Text & "'")
        End If


    End Sub
    Public Sub CaricaImpServiziCanone()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_PF_VOCE AS ID_VOCE,'' AS ID_SERVIZIO,pf_voci.descrizione AS SERVIZIO," _
                                & " (pf_voci.descrizione) AS DESCRIZIONE, '' AS IMPORTO,APPALTI_VOCI_PF.IMPORTO_CANONE " _
                                & " FROM siscom_mi.APPALTI_VOCI_PF, " _
                                & " siscom_mi.pf_voci WHERE  pf_voci.ID = ID_PF_VOCE  AND id_appalto =" & CType(Me.Page, Object).vIdAppalti

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_VARIAZIONI")

            Me.DataGridImpVariazCanone.DataSource = ds
            DataGridImpVariazCanone.DataBind()

            AddJavascriptFunction(DataGridImpVariazCanone, "txtPercVarCanone")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub
    Private Sub AddJavascriptFunction(ByVal Data As DataGrid, ByVal txtname As String)
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To Data.Items.Count - 1
            di = Data.Items(i)
            DirectCast(di.Cells(4).FindControl(txtname), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariaz(this);")
        Next

    End Sub
    '-*-*-*--*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*A T T E N Z I O N E*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    '*********GESTIONE DEL DIV IMPORTI A CONSUMO!!!!*************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!*****************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!**************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!*************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!***************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!******************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!***************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!****************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!*************GESTIONE DEL DIV IMPORTI A CONSUMO!!!!

    Public Sub CaricaImpServiziConsumo()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT ID_PF_VOCE AS ID_VOCE,'' AS ID_SERVIZIO,pf_voci.descrizione AS SERVIZIO," _
                                & " (pf_voci.descrizione) AS DESCRIZIONE, '' AS IMPORTO,APPALTI_VOCI_PF.IMPORTO_CONSUMO AS IMPORTO_CANONE " _
                                & " FROM siscom_mi.APPALTI_VOCI_PF, " _
                                & " siscom_mi.pf_voci WHERE  pf_voci.ID = ID_PF_VOCE  AND id_appalto =" & CType(Me.Page, Object).vIdAppalti

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_VARIAZIONI")

            Me.DataGridImpVariazConsumo.DataSource = ds
            DataGridImpVariazConsumo.DataBind()

            AddJavascriptFunction(DataGridImpVariazConsumo, "txtPercVarConsumo")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub

    Protected Sub btn_ChiudiConsumo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiConsumo.Click
        Me.txtPercVarCons.Text = ""
        Me.txtDataConsumo.Text = ""
        Me.txtNoteConsumo.Text = ""
        txtAppareVC.Value = "0"
        Dim i As Integer = 0
        Dim di As DataGridItem
        For i = 0 To DataGridImpVariazConsumo.Items.Count - 1
            di = DataGridImpVariazConsumo.Items(i)
            DirectCast(di.Cells(4).FindControl("txtPercVarConsumo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariaz(this);")
        Next

    End Sub

    Protected Sub btn_InserisciAppaltiCons_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciAppaltiCons.Click
        Try
            If par.IfEmpty(Me.txtDataConsumo.Text, "") <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                Dim i As Integer = 0

                'CONTROLLO SUI DATI INSERITI IN MODO CHE NON SUPERINO IL 20% previsto dalla legge
                If Me.ConfermaSp_Consumo.Value = 1 Then
                    If CDbl(PercUsataConsumo.Value) + CDbl(txtPercVarCons.Text) > 20 Then
                        Response.Write("<SCRIPT>alert('Attenzione...L\'importo percentuale di variazione, supera quello massimo previsto dalla legge (20%)!\nImporto percentuale di variazione residuo pari a: " & Format((20 - CDbl(PercUsataConsumo.Value)), "##,##0.00") & "%');</SCRIPT>")
                        Response.Write("<SCRIPT>alert('Nessuna Variazione è stata memorizzata!');</SCRIPT>")

                        txtAppareV.Value = "0"
                        Exit Sub
                    End If
                Else
                    Dim DaSommare As Double = 0
                    i = 0
                    For i = 0 To Me.DataGridImpVariazConsumo.Items.Count - 1
                        DaSommare = DaSommare + par.IfEmpty((DirectCast(DataGridImpVariazConsumo.Items(i).Cells(4).FindControl("txtPercVarConsumo"), TextBox).Text), 0)
                    Next
                    DaSommare = DaSommare / DataGridImpVariazConsumo.Items.Count
                    If CDbl(PercUsataConsumo.Value) + DaSommare > 20 Then
                        Response.Write("<SCRIPT>alert('Attenzione...L\'importo percentuale di variazione, supera quello massimo previsto dalla legge (20%)!');</SCRIPT>")
                        txtAppareV.Value = "0"
                        Exit Sub

                    End If
                End If


                '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtDataConsumo.Text) & " AND ID_TIPOLOGIA = 2"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<SCRIPT>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</SCRIPT>")
                    myReader.Close()
                    txtAppareV.Value = "0"
                    Exit Sub
                End If

                Dim IdAppVariazione As Integer = 0
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    IdAppVariazione = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                    & " VALUES(" & IdAppVariazione & ", " & CType(Me.Page, Object).vIdAppalti & ",'" & par.AggiustaData(Me.txtDataConsumo.Text) & "',2,'" & par.PulisciStrSql(Me.txtNoteConsumo.Text.ToUpper) & "')"
                par.cmd.ExecuteNonQuery()

                Dim ImpContSenzaIva As Double = 0
                par.cmd.CommandText = "SELECT SUM ((Importo_Consumo - (Importo_Consumo*(sconto_consumo/100))))  AS ImpContConsumo FROM  siscom_mi.APPALTI_VOCI_PF WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    ImpContSenzaIva = myReader("ImpContConsumo")
                End If
                myReader.Close()


                '****************INIZIO A SALVARE LA VARIAZIONE O LE VARIAZIONI
                Dim di As DataGridItem
                Dim idServizio As Integer = 0
                Dim IdVoceServ As Integer = 0
                Dim Importo As Double = 0

                If Me.ConfermaSp_Consumo.Value = 1 Then
                    '*********************CASO DELLA SPALMABILITA' AUTOMATICA DA PARTE DEL SISTEMA *************
                    If par.IfEmpty(Me.txtPercVarCons.Text, "") <> "" Then
                        Importo = (ImpContSenzaIva * (Me.txtPercVarCons.Text)) / 100
                        Importo = Importo / Me.DataGridImpVariazConsumo.Items.Count

                        For i = 0 To Me.DataGridImpVariazConsumo.Items.Count - 1
                            di = Me.DataGridImpVariazConsumo.Items(i)
                            'idServizio = di.Cells(0).Text
                            IdVoceServ = di.Cells(1).Text
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                & ", " & IdVoceServ & "," & par.VirgoleInPunti(Importo) & "," & par.VirgoleInPunti(par.IfEmpty(Me.txtPercVarCons.Text, 0)) & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                    Else
                        Response.Write("<SCRIPT>alert('I campi data ed importo sono obbligatori!');</SCRIPT>")
                        txtAppareV.Value = "1"
                    End If
                Else

                    '*********************CASO DEL CICLO PER SALVARE LA VARIAZIONE SU ONGI SINGOLA VOCE ******************
                    For i = 0 To DataGridImpVariazConsumo.Items.Count - 1
                        di = Me.DataGridImpVariazConsumo.Items(i)
                        'idServizio = di.Cells(0).Text
                        IdVoceServ = di.Cells(1).Text
                        Importo = (par.IfEmpty(di.Cells(6).Text, 0) * par.IfEmpty((DirectCast(di.Cells(4).FindControl("txtPercVarConsumo"), TextBox).Text), 0)) / 100
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                        & ", " & IdVoceServ & "," & par.VirgoleInPunti(Importo) & "," & par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVarConsumo"), TextBox).Text, 0)) & ")"
                        par.cmd.ExecuteNonQuery()
                    Next

                End If

                Me.txtData.Text = ""
                Me.txtPercVarCons.Text = ""
                Me.txtNote.Text = ""
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                CaricaVarCons()
            Else
                Response.Write("<SCRIPT>alert('I campi data ed importo sono obbligatori!');</SCRIPT>")
                txtAppareV.Value = "0"
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"


        End Try

    End Sub
    Private Sub CaricaVarCons()
        Try
            Try
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_VARIAZIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_VARIAZIONE,PROVVEDIMENTO,TRIM(TO_CHAR(PERCENTUALE,'9G999G999G999G999G990D99')) AS PERCENTUALE,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 2"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "APPALTI_VARIAZIONI")

                Me.DataGridVariazServCons.DataSource = ds
                DataGridVariazServCons.DataBind()


                Dim tot As Double = 0
                par.cmd.CommandText = "SELECT SUM(NVL(percentuale,0)) as PERCVAR FROM siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " AND id_tipologia = 2 AND appalti_variazioni.ID = appalti_variazioni_importi.id_variazione GROUP BY id_variazione"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                For Each row As Data.DataRow In dt.Rows
                    tot = tot + (par.IfNull(row.Item("PERCVAR"), 0) / Me.DataGridImpVariazConsumo.Items.Count)
                Next
                Me.PercUsataConsumo.Value = tot

                'TotPercUsata(ds.Tables("APPALTI_VARIAZIONI"), "CONSUMO")
            Catch ex As Exception
                CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"
            End Try

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub

    Protected Sub DataGridVariazServCons_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVariazServCons.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Variazioni1_txtmia').value='Hai selezionato la variazione sul SERVIZIO a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_Variazioni1_id_selected').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Variazioni1_txtmia').value='Hai selezionato la variazione sul SERVIZIO a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_Variazioni1_id_selected').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    Protected Sub btnEliminaServ_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaServ.Click
        Elimina()

    End Sub

    Protected Sub btnEliminaServCons_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaServCons.Click
        Elimina()

    End Sub
    Private Sub Elimina()
        If Me.txtElimina.Value = 1 Then
            If Me.id_selected.Value > 0 Then
                Try
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans


                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID = " & id_selected.Value
                    par.cmd.ExecuteNonQuery()

                    Me.id_selected.Value = 0
                    Me.txtElimina.Value = 0
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    CaricaVarServizi()
                    CaricaVarCons()


                Catch ex As Exception
                    CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                    CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"
                End Try

            End If
        End If
    End Sub

End Class
