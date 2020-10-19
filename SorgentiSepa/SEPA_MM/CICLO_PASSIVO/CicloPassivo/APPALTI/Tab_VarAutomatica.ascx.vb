
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VarAutomatica
    Inherits System.Web.UI.UserControl
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Me.txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            Me.txtDataConsumo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataConsumo.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            Me.txtPercVarCanone.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariazAuto(this);")
            Me.txtPercVarCons.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariazAuto(this);")

            CaricaImpServiziCanone()
            CaricaImpServiziConsumo()


            CaricaVarServiziCan()
            CaricaVarServiziCons()
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                frmsololettura()
            End If


        End If

    End Sub
    Private Sub frmsololettura()
        btnDelAutoCan.Visible = False
        btnDelAutoCons.Visible = False
    End Sub
    Private Sub AddJavascriptFunction(ByVal Data As DataGrid, ByVal txtname As String)
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To Data.Items.Count - 1
            di = Data.Items(i)
            DirectCast(di.Cells(4).FindControl(txtname), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutodecPercVariazAuto(this);")
        Next

    End Sub
    Public Sub CaricaVarServiziCan()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_VARIAZIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_VARIAZIONE,(SELECT PF_VOCI_IMPORTO.descrizione FROM siscom_mi.PF_VOCI_IMPORTO WHERE ID = id_pf_voce_importo) AS DESC_VOCE,TRIM(TO_CHAR(PERCENTUALE,'9G999G999G999G999G990D99')) AS PERCENTUALE,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 5"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)

            Me.DataGridVCan.DataSource = dt
            DataGridVCan.DataBind()


            Dim tot As Double = 0
            par.cmd.CommandText = "SELECT SUM(NVL(percentuale,0)) as PERCVAR FROM siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " AND id_tipologia = 5 AND appalti_variazioni.ID = appalti_variazioni_importi.id_variazione GROUP BY id_variazione"
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            For Each row As Data.DataRow In dt.Rows
                tot = tot + (par.IfNull(row.Item("PERCVAR"), 0) / Me.DgvServAutoCan.Items.Count)
            Next
            Me.PercUsCanone.Value = tot



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub
    Public Sub CaricaVarServiziCons()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID,to_char(to_date(DATA_VARIAZIONE,'yyyymmdd'),'dd/mm/yyyy') as DATA_VARIAZIONE,(SELECT PF_VOCI_IMPORTO.descrizione FROM siscom_mi.PF_VOCI_IMPORTO WHERE ID = id_pf_voce_importo) AS DESC_VOCE,TRIM(TO_CHAR(PERCENTUALE,'9G999G999G999G999G990D99')) AS PERCENTUALE,TRIM(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.APPALTI_VARIAZIONI, SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI WHERE APPALTI_VARIAZIONI.ID = APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE AND APPALTI_VARIAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_VARIAZIONI.ID_TIPOLOGIA = 6"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)

            Me.DataGridVCons.DataSource = dt
            DataGridVCons.DataBind()


            Dim tot As Double = 0
            par.cmd.CommandText = "SELECT SUM(NVL(percentuale,0)) as PERCVAR FROM siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " AND id_tipologia = 6 AND appalti_variazioni.ID = appalti_variazioni_importi.id_variazione GROUP BY id_variazione"
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            For Each row As Data.DataRow In dt.Rows
                tot = tot + (par.IfNull(row.Item("PERCVAR"), 0) / Me.DgvServAutoCan.Items.Count)
            Next
            Me.PercUsCons.Value = tot



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub

    Public Sub CaricaImpServiziCanone()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO AS ID_VOCE,TAB_SERVIZI.ID AS ID_SERVIZIO,TAB_SERVIZI.DESCRIZIONE as SERVIZIO,(pf_voci_importo.descrizione) AS DESCRIZIONE, '' AS IMPORTO,APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE FROM siscom_mi.appalti_lotti_servizi, siscom_mi.tab_servizi, siscom_mi.pf_voci_importo WHERE  pf_voci_importo.ID = ID_PF_VOCE_IMPORTO AND tab_servizi.ID = pf_voci_importo.ID_servizio AND id_appalto = " & CType(Me.Page, Object).vIdAppalti

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)

            Me.DgvServAutoCan.DataSource = dt
            DgvServAutoCan.DataBind()

            AddJavascriptFunction(DgvServAutoCan, "txtImpCanone")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub

    Public Sub CaricaImpServiziConsumo()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO AS ID_VOCE,TAB_SERVIZI.ID AS ID_SERVIZIO,TAB_SERVIZI.DESCRIZIONE as SERVIZIO,(pf_voci_importo.descrizione) AS DESCRIZIONE, '' AS IMPORTO,APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO AS IMPORTO_CANONE FROM siscom_mi.appalti_lotti_servizi, siscom_mi.tab_servizi, siscom_mi.pf_voci_importo WHERE  pf_voci_importo.ID = ID_PF_VOCE_IMPORTO AND tab_servizi.ID = pf_voci_importo.ID_servizio AND id_appalto = " & CType(Me.Page, Object).vIdAppalti
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)

            Me.DgvServAutoCons.DataSource = dt
            DgvServAutoCons.DataBind()

            AddJavascriptFunction(DgvServAutoCons, "txtPercVarCons")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"

        End Try
    End Sub

    Protected Sub btn_AddVariazAutoCan_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_AddVariazAutoCan.Click
        SalvaAutoCan()
    End Sub
    Private Sub SalvaAutoCan()
        Try
            If par.IfEmpty(Me.txtData.Text, "") <> "" Then
                If par.AggiustaData(par.IfEmpty(Me.txtData.Text, 0)) < par.AggiustaData(DirectCast(Me.Page.FindControl("txtannoinizio"), TextBox).Text) Or par.AggiustaData(Me.txtData.Text) > par.AggiustaData(DirectCast(Me.Page.FindControl("txtannofine"), TextBox).Text) Then
                    Response.Write("<SCRIPT>alert('La data della variazione deve essere compresa nel periodo di durata dell\'appalto!');</SCRIPT>")
                    Exit Sub
                End If

                Dim i As Integer = 0

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                'CONTROLLO SUI DATI INSERITI 
                If par.IfEmpty(Me.SpalmCanone.Value, 0) = 1 Then
                    If String.IsNullOrEmpty(Me.txtData.Text) Or String.IsNullOrEmpty(Me.txtPercVarCanone.Text) Then
                        Response.Write("<script>alert('I campi data ed importo sono obbligatori!');</script>")
                        Exit Sub
                    End If
                Else
                    'Dim di As DataGridItem
                    'For i = 0 To DgvServAutoCan.Items.Count - 1
                    '    di = Me.DgvServAutoCan.Items(i)

                    '    If par.IfEmpty((DirectCast(di.Cells(4).FindControl("txtImpCanone"), TextBox).Text), 0) = 0 Then
                    '        Response.Write("<script>alert('Attenzione...Valorizzare tutti gli importi!');</script>")
                    '        Exit Sub
                    '    End If
                    'Next
                End If

                '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtData.Text) & " AND ID_TIPOLOGIA = 5"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<script>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</script>")
                    myReader.Close()
                    Exit Sub
                End If




                Dim ImpContSenzaIva As Decimal = 0
                par.cmd.CommandText = "SELECT SUM ((Importo_canone - (Importo_canone*(sconto_canone/100))))  AS ImpContCanone FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    ImpContSenzaIva = myReader("ImpContCanone")
                End If
                myReader.Close()

                If ImpContSenzaIva > 0 Then
                    Dim IdAppVariazione As Integer = 0
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        IdAppVariazione = myReader(0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                        & " VALUES(" & IdAppVariazione & ", " & CType(Me.Page, Object).vIdAppalti & ",'" & par.AggiustaData(Me.txtData.Text) & "',5,'" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()

                    '****************INIZIO A SALVARE LA VARIAZIONE O LE VARIAZIONI
                    i = 0
                    Dim di As DataGridItem
                    Dim idServizio As Integer = 0
                    Dim IdVoceServ As Integer = 0
                    Dim Importo As Decimal = 0

                    If par.IfEmpty(Me.SpalmCanone.Value, 0) = 1 Then
                        '*********************CASO DELLA SPALMABILITA' AUTOMATICA DA PARTE DEL SISTEMA *************
                        If par.IfEmpty(Me.txtPercVarCanone.Text, "") <> "" Then
                            'Importo = (ImpContSenzaIva * (Me.txtPercVarCanone.Text)) / 100
                            'Importo = Importo / Me.DgvServAutoCan.Items.Count

                            For i = 0 To Me.DgvServAutoCan.Items.Count - 1
                                ImpContSenzaIva = 0
                                Importo = 0
                                di = Me.DgvServAutoCan.Items(i)
                                idServizio = di.Cells(0).Text
                                IdVoceServ = di.Cells(1).Text
                                par.cmd.CommandText = "SELECT SUM ((Importo_canone - (Importo_canone*(sconto_canone/100))))  AS ImpContCanone FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " and ID_PF_VOCE_IMPORTO = " & IdVoceServ
                                myReader = par.cmd.ExecuteReader
                                If myReader.Read Then
                                    ImpContSenzaIva = par.IfNull(myReader("ImpContCanone"), 0)
                                End If
                                myReader.Close()
                                If ImpContSenzaIva > 0 Then
                                    Importo = Math.Round(CDec((ImpContSenzaIva * (Me.txtPercVarCanone.Text)) / 100), 2)
                                    'Importo = Importo / Me.DgvServAutoCan.Items.Count
                                End If
                                If Importo <> 0 Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                                        & ", " & IdVoceServ & "," & par.VirgoleInPunti(Importo) & "," & par.VirgoleInPunti(par.IfEmpty(Me.txtPercVarCanone.Text, 0)) & ")"
                                    par.cmd.ExecuteNonQuery()

                                End If
                            Next
                        End If
                    Else

                        '*********************CASO DEL CICLO PER SALVARE LA VARIAZIONE SU ONGI SINGOLA VOCE ******************
                        Dim divisore As Decimal = 1
                        For i = 0 To DgvServAutoCan.Items.Count - 1
                            di = Me.DgvServAutoCan.Items(i)
                            If par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtImpCanone"), TextBox).Text, 0)) <> 0 Then
                                idServizio = di.Cells(0).Text
                                IdVoceServ = di.Cells(1).Text
                                divisore = 1
                                If CDec(par.IfEmpty(di.Cells(6).Text, 1)) = 0 Then
                                    divisore = 1
                                Else
                                    divisore = CDec(par.IfEmpty(di.Cells(6).Text, 1))
                                End If
                                Importo = Math.Round(CDec(par.IfEmpty((DirectCast(di.Cells(4).FindControl("txtImpCanone"), TextBox).Text), 0)) * 100 / divisore, 2)

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                & ", " & IdVoceServ & "," & par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtImpCanone"), TextBox).Text, 0)) & "," & par.VirgoleInPunti(Importo) & ")"
                                par.cmd.ExecuteNonQuery()

                            End If
                        Next

                    End If

                    SalvaPluriennali(IdAppVariazione, 5)
                    CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()

                    CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaResiduo()
                Else
                    Response.Write("<script>alert('Nessun importo a Canone per questo appalto!');</script>")
                    Me.txtData.Text = ""
                    Me.txtPercVarCanone.Text = ""
                    Me.txtNote.Text = ""
                    hfRestaVisible.Value = 0

                End If

                Me.txtData.Text = ""
                Me.txtPercVarCanone.Text = ""
                Me.txtNote.Text = ""
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                CaricaImpServiziCanone()
                CaricaVarServiziCan()
                'End If
                hfRestaVisible.Value = 0
            Else
                Response.Write("<script>alert('I campi data ed importo sono obbligatori!');</script>")
                hfRestaVisible.Value = 1
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"
            par.myTrans.Rollback()

        End Try
    End Sub

    Protected Sub btn_ChiudiAppalti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiAppalti.Click
        Me.txtPercVarCanone.Text = ""
        Me.txtData.Text = ""
        Me.txtNote.Text = ""
        hfRestaVisible.Value = "0"
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To DgvServAutoCan.Items.Count - 1
            di = DgvServAutoCan.Items(i)
            DirectCast(di.Cells(4).FindControl("txtImpCanone"), TextBox).Text = ""
        Next

    End Sub

    Protected Sub btn_AddVariazAutoCon_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_AddVariazAutoCon.Click
        SalvaAutoCons()
    End Sub
    Private Sub SalvaAutoCons()
        Try
            If par.IfEmpty(Me.txtDataConsumo.Text, "") <> "" Then
                If par.AggiustaData(par.IfEmpty(Me.txtDataConsumo.Text, 0)) < par.AggiustaData(DirectCast(Me.Page.FindControl("txtannoinizio"), TextBox).Text) Or par.AggiustaData(par.IfEmpty(txtDataConsumo.Text, 0)) > par.AggiustaData(DirectCast(Me.Page.FindControl("txtannofine"), TextBox).Text) Then
                    Response.Write("<SCRIPT>alert('La data della variazione deve essere compresa nel periodo di durata dell\'appalto!');</SCRIPT>")
                    Exit Sub
                End If
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Dim i As Integer = 0
                'CONTROLLO SUI DATI INSERITI 
                If par.IfEmpty(Me.SpalmCons.Value, 0) = 1 Then
                    If String.IsNullOrEmpty(Me.txtDataConsumo.Text) Or String.IsNullOrEmpty(Me.txtPercVarCons.Text) Then
                        Response.Write("<script>alert('I campi data ed importo sono obbligatori!');</script>")
                        Exit Sub
                    End If
                Else
                    'Dim di As DataGridItem
                    'For i = 0 To DgvServAutoCons.Items.Count - 1
                    '    di = Me.DgvServAutoCons.Items(i)

                    '    If par.IfEmpty((DirectCast(di.Cells(4).FindControl("txtPercVarCons"), TextBox).Text), 0) = 0 Then
                    '        Response.Write("<script>alert('Attenzione...Valorizzare tutti gli importi!');</script>")
                    '        Exit Sub
                    '    End If
                    'Next
                End If

                i = 0



                '*********CONTROLLO NON ESISTA GIà UNA VARIAZIONE CON LA STESSA DATA SUL CONTRATTO
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND DATA_VARIAZIONE = " & par.AggiustaData(Me.txtDataConsumo.Text) & " AND ID_TIPOLOGIA = 6"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<script>alert('Attenzione...Variazione esistente su questo appalto e con la stessa data!');</script>")
                    myReader.Close()
                    Exit Sub
                End If




                Dim ImpContSenzaIva As Decimal = 0
                par.cmd.CommandText = "SELECT SUM ((Importo_CONSUMO - (Importo_CONSUMO*(sconto_consumo/100))))  AS ImpContConsumo FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    ImpContSenzaIva = myReader("ImpContConsumo")
                End If
                myReader.Close()

                If ImpContSenzaIva > 0 Then
                    Dim IdAppVariazione As Integer = 0
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        IdAppVariazione = myReader(0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                        & " VALUES(" & IdAppVariazione & ", " & CType(Me.Page, Object).vIdAppalti & ",'" & par.AggiustaData(Me.txtDataConsumo.Text) & "',6,'" & par.PulisciStrSql(Me.txtNoteConsumo.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()

                    '****************INIZIO A SALVARE LA VARIAZIONE O LE VARIAZIONI
                    i = 0
                    Dim di As DataGridItem
                    Dim idServizio As Integer = 0
                    Dim IdVoceServ As Integer = 0
                    Dim Importo As Decimal = 0

                    If par.IfEmpty(Me.SpalmCons.Value, 0) = 1 Then
                        '*********************CASO DELLA SPALMABILITA' AUTOMATICA DA PARTE DEL SISTEMA *************
                        If par.IfEmpty(Me.txtPercVarCons.Text, "") <> "" Then
                            'Importo = (ImpContSenzaIva * (Me.txtPercVarCons.Text)) / 100
                            'Importo = Importo / Me.DgvServAutoCons.Items.Count

                            For i = 0 To Me.DgvServAutoCons.Items.Count - 1
                                ImpContSenzaIva = 0
                                Importo = 0

                                di = Me.DgvServAutoCons.Items(i)
                                idServizio = di.Cells(0).Text
                                IdVoceServ = di.Cells(1).Text
                                par.cmd.CommandText = "SELECT SUM ((Importo_CONSUMO - (Importo_CONSUMO*(sconto_consumo/100))))  AS ImpContConsumo FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto = " & CType(Me.Page, Object).vIdAppalti & " and ID_PF_VOCE_IMPORTO = " & IdVoceServ
                                myReader = par.cmd.ExecuteReader
                                If myReader.Read Then
                                    ImpContSenzaIva = par.IfNull(myReader("ImpContConsumo"), 0)
                                End If
                                myReader.Close()
                                If ImpContSenzaIva > 0 Then
                                    Importo = Math.Round(CDec((ImpContSenzaIva * (Me.txtPercVarCons.Text)) / 100), 2)
                                    'Importo = Importo / Me.DgvServAutoCons.Items.Count
                                End If
                                If Importo <> 0 Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                                        & ", " & IdVoceServ & "," & par.VirgoleInPunti(Importo) & "," & par.VirgoleInPunti(par.IfEmpty(Me.txtPercVarCons.Text, 0)) & ")"
                                    par.cmd.ExecuteNonQuery()

                                End If

                            Next
                        End If
                    Else
                        Dim divisore As Integer = 1
                        '*********************CASO DEL CICLO PER SALVARE LA VARIAZIONE SU ONGI SINGOLA VOCE ******************
                        For i = 0 To DgvServAutoCons.Items.Count - 1
                            divisore = 1
                            di = Me.DgvServAutoCons.Items(i)
                            If par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVarCons"), TextBox).Text, 0)) <> 0 Then

                                idServizio = di.Cells(0).Text
                                IdVoceServ = di.Cells(1).Text
                                If CDec(par.IfEmpty(di.Cells(6).Text, 1)) = 0 Then
                                    divisore = 1
                                Else
                                    divisore = CDec(par.IfEmpty(di.Cells(6).Text, 1))
                                End If

                                Importo = Math.Round(CDec(par.IfEmpty((DirectCast(di.Cells(4).FindControl("txtPercVarCons"), TextBox).Text), 0)) * 100 / divisore, 2)

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                & ", " & IdVoceServ & "," & par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(4).FindControl("txtPercVarCons"), TextBox).Text, 0)) & "," & par.VirgoleInPunti(Importo) & ")"
                                par.cmd.ExecuteNonQuery()
                            End If

                        Next

                    End If

                    SalvaPluriennali(IdAppVariazione, 6)
                    CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaImpContrattuale()
                    CType(Me.Page.FindControl("Tab_Servizio"), Object).CalcolaResiduo()

                Else
                    Response.Write("<script>alert('Nessun importo a Consumo per questo appalto!');</script>")
                    Me.txtDataConsumo.Text = ""
                    Me.txtPercVarCons.Text = ""
                    Me.txtNoteConsumo.Text = ""
                    hfRestaVisibleCon.Value = 0

                End If

                Me.txtDataConsumo.Text = ""
                Me.txtPercVarCons.Text = ""
                Me.txtNoteConsumo.Text = ""
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                CaricaImpServiziConsumo()
                CaricaVarServiziCons()
                'End If
                hfRestaVisibleCon.Value = 0
            Else
                Response.Write("<script>alert('I campi data ed importo sono obbligatori!');</script>")
                hfRestaVisibleCon.Value = 1
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans


            'par.myTrans.Rollback()


        End Try
    End Sub

    Protected Sub btn_ChiudiConsumo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiConsumo.Click
        Me.txtPercVarCons.Text = ""
        Me.txtDataConsumo.Text = ""
        Me.txtNoteConsumo.Text = ""
        hfRestaVisibleCon.Value = "0"
        Dim i As Integer = 0
        Dim di As DataGridItem

        For i = 0 To DgvServAutoCons.Items.Count - 1
            di = DgvServAutoCons.Items(i)
            DirectCast(di.Cells(4).FindControl("txtPercVarCons"), TextBox).Text = ""
        Next

    End Sub
    Private Sub Elimina()
        If Me.hfElimina.Value = 1 Then
            If Me.idSelected.Value > 0 Then
                Try
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans


                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID = " & idSelected.Value
                    par.cmd.ExecuteNonQuery()

                    Me.idSelected.Value = 0
                    Me.hfElimina.Value = 0
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                    CaricaVarServiziCan()
                    CaricaVarServiziCons()


                Catch ex As Exception
                    CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                    CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVariazioneServizi"
                    par.myTrans.Rollback()

                End Try

            End If
        End If
    End Sub

    Protected Sub DataGridVCan_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVCan.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VarAutomatica1_txtmia').value='Hai selezionato la variazione a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VarAutomatica1_idSelected').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VarAutomatica1_txtmia').value='Hai selezionato la variazione a CANONE del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VarAutomatica1_idSelected').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    Protected Sub DataGridVCons_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVCons.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VarAutomatica1_txtmia').value='Hai selezionato la variazione a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VarAutomatica1_idSelected').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VarAutomatica1_txtmia').value='Hai selezionato la variazione a CONSUMO del " & e.Item.Cells(1).Text.Replace("'", "\'") & " ';document.getElementById('Tab_VarAutomatica1_idSelected').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    Protected Sub btnDelAutoCan_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelAutoCan.Click
        Elimina()
    End Sub

    Protected Sub btnDelAutoCons_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelAutoCons.Click
        Elimina()
    End Sub
    Private Sub SalvaPluriennali(ByVal idVariazione As Integer, ByVal idTipo As Integer)


        'Verifico se l'appalto è pluriennale
        par.cmd.CommandText = "select id as id_appalto,'' as ID_PF_VOCE_IMPORTO from siscom_mi.appalti where id_gruppo = " _
                    & "(select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " ) order by id_appalto asc"
        Dim daAppalti As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtAppalti As New Data.DataTable()
        daAppalti.Fill(dtAppalti)
        daAppalti.Dispose()




        If dtAppalti.Rows.Count > 1 Then
            Dim rIApp As Integer = 0
            Dim reader As Oracle.DataAccess.Client.OracleDataReader
            '***********************  creo la datatable sulla quale ciclare per creare le variazioni sugli appalti pluriennali 
            par.cmd.CommandText = "select '' as id_appalto,'' as ID_PF_VOCE_IMPORTO,0 as importo,0 as PERCENTUALE from dual"
            Dim daInsert As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtInsert As New Data.DataTable()
            daInsert.Fill(dtInsert)


            'tengo in memoria le pfVociImporto variate per l'appalto in uso
            par.cmd.CommandText = "select ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE from siscom_mi.appalti_variazioni_importi where id_variazione = " & idVariazione
            Dim daPfVociImporti As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtPfVoci As New Data.DataTable()
            daPfVociImporti.Fill(dtPfVoci)
            daPfVociImporti.Dispose()



            'TROVO L'INDICE DELL'APPALTO IN CUI STO ESEGUENDO LA VARIAZIONE
            For Each r As Data.DataRow In dtAppalti.Rows
                If r.Item("ID_APPALTO") = CType(Me.Page, Object).vIdAppalti Then
                    Exit For
                End If
                rIApp += 1
            Next
            dtInsert.Rows.RemoveAt(0)
            dtInsert.AcceptChanges()
            '...dal basso verso il contratto in uso nella maschera....
            For i As Integer = dtAppalti.Rows.Count - 1 To rIApp Step -1
                If dtAppalti.Rows(i).Item("id_appalto") <> CType(Me.Page, Object).vIdAppalti Then
                    'cerco tutti gli id_pf_voce_importo dello stesso appalto registrato in anni precedenti
                    For Each r As Data.DataRow In dtPfVoci.Rows
                        par.cmd.CommandText = "SELECT id FROM siscom_mi.PF_VOCI_IMPORTO WHERE ID = (SELECT id_old FROM siscom_mi.PF_VOCI_IMPORTO WHERE ID =" & r.Item("ID_PF_VOCE_IMPORTO").ToString & ")"
                        reader = par.cmd.ExecuteReader
                        If reader.Read Then
                            Dim rAggiunta As Data.DataRow
                            rAggiunta = dtInsert.NewRow()
                            rAggiunta.Item("id_appalto") = dtAppalti.Rows(i).Item("id_appalto")
                            rAggiunta.Item("ID_PF_VOCE_IMPORTO") = par.IfNull(reader("id"), "")
                            rAggiunta.Item("importo") = par.IfEmpty(r.Item("importo").ToString, 0)
                            rAggiunta.Item("percentuale") = par.IfEmpty(r.Item("percentuale").ToString, 0)


                            dtInsert.Rows.Add(rAggiunta)
                            dtInsert.AcceptChanges()
                        End If
                        reader.Close()
                    Next
                Else
                    Exit For
                End If
            Next

            '...dall'allto fino al contratto in uso nella maschera....
            For i As Integer = 0 To rIApp
                If dtAppalti.Rows(i).Item("id_appalto") <> CType(Me.Page, Object).vIdAppalti Then
                    'cerco tutti gli id_pf_voce_importo dello stesso appalto registrato in anni precedenti
                    For Each r As Data.DataRow In dtPfVoci.Rows
                        par.cmd.CommandText = "SELECT id FROM siscom_mi.PF_VOCI_IMPORTO WHERE ID_OLD = " & r.Item("ID_PF_VOCE_IMPORTO").ToString
                        reader = par.cmd.ExecuteReader
                        If reader.Read Then
                            Dim rAggiunta As Data.DataRow
                            rAggiunta = dtInsert.NewRow()
                            rAggiunta.Item("id_appalto") = dtAppalti.Rows(i).Item("id_appalto")
                            rAggiunta.Item("ID_PF_VOCE_IMPORTO") = par.IfNull(reader("id"), "")
                            rAggiunta.Item("importo") = par.IfEmpty(r.Item("importo").ToString, 0)
                            rAggiunta.Item("percentuale") = par.IfEmpty(r.Item("percentuale").ToString, 0)

                            dtInsert.Rows.Add(rAggiunta)
                            dtInsert.AcceptChanges()
                        End If
                        reader.Close()
                    Next
                Else
                    Exit For
                End If
            Next

            Dim idappaltoIns As Integer = 0
            If dtInsert.Rows.Count > 0 Then
                Dim IdAppVariazione As Integer = 0

                For Each rInsert As Data.DataRow In dtInsert.Rows
                    If idappaltoIns <> par.IfEmpty(rInsert.Item("id_appalto").ToString, 0) Then
                        idappaltoIns = rInsert.Item("id_appalto")
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI_VARIAZIONI.NEXTVAL FROM DUAL"
                        reader = par.cmd.ExecuteReader
                        If reader.Read Then
                            IdAppVariazione = reader(0)
                        End If
                        reader.Close()
                        If idTipo = 6 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                                                & " VALUES(" & IdAppVariazione & ", " & idappaltoIns & ",'" & par.AggiustaData(Me.txtDataConsumo.Text) & "'," & idTipo & ",'" & par.PulisciStrSql(Me.txtNoteConsumo.Text.ToUpper) & "')"
                            par.cmd.ExecuteNonQuery()
                        ElseIf idTipo = 5 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI(ID,ID_APPALTO,DATA_VARIAZIONE,ID_TIPOLOGIA,PROVVEDIMENTO)" _
                                                & " VALUES(" & IdAppVariazione & ", " & idappaltoIns & ",'" & par.AggiustaData(Me.txtData.Text) & "'," & idTipo & ",'" & par.PulisciStrSql(Me.txtNote.Text.ToUpper) & "')"
                            par.cmd.ExecuteNonQuery()

                        End If

                    End If
                    If IdAppVariazione > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI (ID_VARIAZIONE,ID_PF_VOCE_IMPORTO,IMPORTO,PERCENTUALE) VALUES (" & IdAppVariazione _
                                            & ", " & rInsert.Item("id_pf_voce_importo") & "," & par.VirgoleInPunti(rInsert.Item("importo")) & "," & par.VirgoleInPunti(rInsert.Item("percentuale")) & ")"
                        par.cmd.ExecuteNonQuery()

                    End If


                Next
            End If



        End If


    End Sub

End Class
