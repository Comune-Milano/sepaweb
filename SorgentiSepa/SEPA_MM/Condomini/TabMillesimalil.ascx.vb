
Partial Class Condomini_TabMillesimalil
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'RiempiCamp cercava i millesimi totali inseriti a livello di condominio ora non serve più perchè li calcola su quelli degli inquilini.
                RiempiCampi()

                'CHIAMA CALCOLA PERCENTUALE DOPO AVER INSERITO IL TOTALE MILLESIMO
                txtMilProp.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this);javascript:AutoDecimal(this);")
                txtMillComp.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this);javascript:AutoDecimal(this);")
                TxtMillSup.Attributes.Add("onBlur", "javascript:CalcolaPercentualeMill(this);javascript:AutoDecimal(this);")
                txtMillGest.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this);javascript:AutoDecimal(this);")
                '*****PEPPE MODIFY 05/01/2011
                txtMillPres.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this);javascript:AutoDecimal(this);")
                TxtTotScale.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');")


                'CHIAMA CALCOLAPERCENTUALE2
                txtMilPropComune.Attributes.Add("onBlur", "javascript:CalcolaPercentuale2(this);")
                txtMillCompComune.Attributes.Add("onBlur", "javascript:CalcolaPercentuale2(this);")
                TxtMillSupComune.Attributes.Add("onBlur", "javascript:CalcolaPercentualeMill2(this);")
                txtMillGestComune.Attributes.Add("onBlur", "javascript:CalcolaPercentuale2(this);")
                '*****PEPPE MODIFY 05/01/2011
                txtMillPresComune.Attributes.Add("onBlur", "javascript:CalcolaPercentuale2(this);")

                txtMilPropComune.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                txtMillCompComune.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                'TxtMillSupComune.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                txtMillGestComune.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                '*****PEPPE MODIFY 05/01/2011
                txtMillPresComune.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")

                'txtMilProp.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                'txtMillComp.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                ''TxtMillSup.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                'txtMillGest.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                ''*****PEPPE MODIFY 05/01/2011
                ''txtMillPres.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")

                txtPropCondomominio.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")
                txtMilScaleCondominio.Attributes.Add("onkeyup", "javascript:MaxMillesimo(this);")

                txtPropCondFabb.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txtPropCondomominio.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
                txtMilScaleCondominio.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")

                If DirectCast(Me.Page.FindControl("ImgVisibility"), HiddenField).Value <> 1 Then
                    Me.btnVisualizza.Visible = False
                    Me.btnDelete.Visible = False
                    Me.BtnVisualEdif.Visible = False
                End If

            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMillesimalil"
        End Try

    End Sub
    Private Sub RiempiCampi()
        Try
            If CType(Me.Page, Object).vIdCondominio() <> "" Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "SELECT MIL_PRO_TOT_COM ,MIL_COMPRO_TOT_COM,MIL_SUP_TOT_COM,MIL_GEST_TOT_COM, MIL_PRO_TOT_COND, MIL_COMPRO_TOT_COND, MIL_SUP_TOT_COND, MIL_GEST_TOT_COND FROM SISCOM_MI.CONDOMINI WHERE ID = " & CType(Me.Page, Object).vIdCondominio()
                par.cmd.CommandText = "SELECT  TO_CHAR(MIL_PRO_TOT_COND,'9999990D9999') AS MIL_PRO_TOT_COND, TO_CHAR(MIL_COMPRO_TOT_COND,'9999990D9999') AS MIL_COMPRO_TOT_COND, TO_CHAR(MIL_SUP_TOT_COND,'9999990D9999')AS MIL_SUP_TOT_COND, TO_CHAR(MIL_GEST_TOT_COND,'9999990D9999')AS MIL_GEST_TOT_COND, TO_CHAR(MIL_PRES_ASS_TOT_COND,'9999990D9999')AS MIL_PRES_ASS_TOT_COND  FROM SISCOM_MI.CONDOMINI WHERE ID = " & CType(Me.Page, Object).vIdCondominio()
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    ' Me.txtMilPropComune.Text = par.PuntiInVirgole(myReader1("MIL_PRO_TOT_COM").ToString)
                    Me.txtMilProp.Text = Trim(par.PuntiInVirgole(myReader1("MIL_PRO_TOT_COND").ToString))
                    'Me.txtMillCompComune.Text = par.PuntiInVirgole(myReader1("MIL_COMPRO_TOT_COM").ToString)
                    Me.txtMillComp.Text = Trim(par.PuntiInVirgole(myReader1("MIL_COMPRO_TOT_COND").ToString))
                    'Me.TxtMillSupComune.Text = par.PuntiInVirgole(myReader1("MIL_SUP_TOT_COM").ToString)
                    Me.TxtMillSup.Text = Trim(par.PuntiInVirgole(myReader1("MIL_SUP_TOT_COND").ToString))
                    'Me.txtMillGestComune.Text = par.PuntiInVirgole(myReader1("MIL_GEST_TOT_COM").ToString)
                    Me.txtMillGest.Text = Trim(par.PuntiInVirgole(myReader1("MIL_GEST_TOT_COND").ToString))
                    Me.txtMillPres.Text = Trim(par.PuntiInVirgole(myReader1("MIL_PRES_ASS_TOT_COND").ToString))

                End If
                'CalcolaPercentuale()
                'CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtPercMilProp"), TextBox).Text = txtMilPropComunePerc.Text
                'CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtpercSup"), TextBox).Text = TxtMillSupComunePerc.Text
                'CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtPercMillComp"), TextBox).Text = txtMillCompComunePerc.Text
                'CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtPercMillPresAss"), TextBox).Text = txtMillPresComunePerc.Text

                CercaScale()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMillesimalil"
        End Try

    End Sub
    Public Sub CalcolaSOMMAMillesimi()
        Try
            If CType(Me.Page, Object).vIdCondominio <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "SELECT SUM(MIL_PRO)AS MIL_PRO, SUM(MIL_COMPRO) AS MIL_COMPRO, SUM(MIL_RISC) AS MIL_SUP, SUM(MIL_GEST) AS MIL_GEST FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO =" & CType(Me.Page, Object).vIdCondominio
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    Me.txtMilPropComune.Text = Format(par.IfNull(CDbl(myReader1("MIL_PRO")), 0), "0.0000")
                '    Me.txtMillCompComune.Text = Format((par.IfNull(CDbl(myReader1("MIL_COMPRO")), 0)), "0.0000")
                '    Me.TxtMillSupComune.Text = Format((par.IfNull(CDbl(myReader1("MIL_SUP")), 0)), "0.0000")
                '    Me.txtMillGestComune.Text = Format((par.IfNull(CDbl(myReader1("MIL_GEST")), 0)), "0.0000")
                'End If
                'myReader1.Close()
                '**********PEPPE MODIFY 05/01/2011 AGGIUNTO MILL_PRES_ASS
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(MIL_PRO),'9999990D9999')AS MIL_PRO, TO_CHAR(SUM(MIL_COMPRO),'9999990D9999')AS MIL_COMPRO, TO_CHAR(SUM(MIL_RISC),'9999990D9999')AS MIL_SUP, TO_CHAR(SUM(MIL_GEST),'9999990D9999')AS MIL_GEST,TO_CHAR(SUM(MILL_PRES_ASS),'9999990D9999')AS MILL_PRES_ASS  FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO =" & CType(Me.Page, Object).vIdCondominio
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtMilPropComune.Text = Trim(par.IfNull(myReader1("MIL_PRO"), ""))
                    Me.txtMillCompComune.Text = Trim(par.IfNull(myReader1("MIL_COMPRO"), ""))
                    Me.TxtMillSupComune.Text = Trim(par.IfNull(myReader1("MIL_SUP"), ""))
                    Me.txtMillGestComune.Text = Trim(par.IfNull(myReader1("MIL_GEST"), ""))
                    Me.txtMillPresComune.Text = Trim(par.IfNull(myReader1("MILL_PRES_ASS"), ""))
                End If
                myReader1.Close()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMillesimalil"
        End Try

    End Sub


    Public Sub CercaScale()
        Try
            'If DirectCast(Me.Page.FindControl("DrlEdificio"), DropDownList).SelectedValue <> "-1" Then
            If CType(Me.Page, Object).vSelezionati() = 1 Then
                'VELOCIZZAZIONE 10/08/2010
                'If CType(Me.Page, Object).Selezionati = True Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "SELECT EDIFICI.COD_EDIFICIO,EDIFICI.DENOMINAZIONE AS EDIFICIO, COND_MIL_SCALE.ID_SCALA,COND_MIL_SCALE.ID_CONDOMINIO,TO_CHAR(COND_MIL_SCALE.MIL_ASC_COND,'9G999G990D9999')AS MIL_ASC_COND,TO_CHAR(COND_MIL_SCALE.MIL_PROP_COND,'9G999G990D9999') AS MIL_PROP_COND,SCALE_EDIFICI.DESCRIZIONE,TO_CHAR((SELECT SUM(MIL_ASC)FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA),'9G999G990D9999') AS MIL_ASC_TOT,TO_CHAR((SELECT SUM(MIL_PRO) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA),'9G999G990D9999') AS MIL_PRO_TOT,TO_CHAR((CASE WHEN COND_MIL_SCALE.MIL_ASC_COND =0 THEN 0 ELSE((100*(SELECT SUM(MIL_ASC) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA))/COND_MIL_SCALE.MIL_ASC_COND)END),'9G999G990D9999') AS PERCENTASC,TO_CHAR((CASE WHEN COND_MIL_SCALE.MIL_PROP_COND =0 THEN 0 ELSE((100*(SELECT SUM(MIL_PRO) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA))/COND_MIL_SCALE.MIL_PROP_COND)END),'9G999G990D9999') AS PERCENTPROP FROM SISCOM_MI.COND_MIL_SCALE, SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.COND_EDIFICI, SISCOM_MI.EDIFICI WHERE COND_MIL_SCALE.FL_VISIBILE= 1 AND SCALE_EDIFICI.ID = ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO AND SCALE_EDIFICI.ID_EDIFICIO = EDIFICI.ID AND COND_EDIFICI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_MIL_SCALE.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " ORDER BY DESCRIZIONE ASC"
                par.cmd.CommandText = "SELECT EDIFICI.COD_EDIFICIO,EDIFICI.DENOMINAZIONE AS EDIFICIO, " _
                                    & "COND_MIL_SCALE.ID_SCALA,COND_MIL_SCALE.ID_CONDOMINIO," _
                                    & "TO_CHAR(COND_MIL_SCALE.MIL_ASC_COND,'9G999G990D9999')AS MIL_ASC_COND," _
                                    & "TO_CHAR(COND_MIL_SCALE.MIL_PROP_COND,'9G999G990D9999') AS MIL_PROP_COND," _
                                    & "SCALE_EDIFICI.DESCRIZIONE," _
                                    & "TO_CHAR((SELECT SUM(MIL_ASC)FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA),'9G999G990D9999') AS MIL_ASC_TOT," _
                                    & "TO_CHAR((SELECT SUM(MIL_PRO) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA),'9G999G990D9999') AS MIL_PRO_TOT," _
                                    & "TO_CHAR((CASE WHEN COND_MIL_SCALE.MIL_ASC_COND =0 THEN 0 " _
                                    & "			  ELSE((100*(SELECT SUM(MIL_ASC)FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID " _
                                    & "			  AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA))/COND_MIL_SCALE.MIL_ASC_COND)END),'9G999G990D9999') AS PERCENTASC," _
                                    & "TO_CHAR((CASE WHEN COND_MIL_SCALE.MIL_PROP_COND =0 THEN 0 " _
                                    & "			  ELSE((100*(SELECT SUM(MIL_PRO) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI " _
                                    & "			  WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID " _
                                    & "			  AND UNITA_IMMOBILIARI.ID_SCALA = COND_MIL_SCALE.ID_SCALA))/COND_MIL_SCALE.MIL_PROP_COND)END),'9G999G990D9999') AS PERCENTPROP " _
                                    & "FROM " _
                                    & "SISCOM_MI.COND_MIL_SCALE, " _
                                    & "SISCOM_MI.SCALE_EDIFICI, " _
                                    & "SISCOM_MI.COND_EDIFICI, " _
                                    & "SISCOM_MI.EDIFICI " _
                                    & "WHERE " _
                                    & "COND_MIL_SCALE.FL_VISIBILE= 1 " _
                                    & "AND SCALE_EDIFICI.ID = ID_SCALA " _
                                    & "AND SCALE_EDIFICI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO " _
                                    & "AND SCALE_EDIFICI.ID_EDIFICIO = EDIFICI.ID " _
                                    & "AND COND_EDIFICI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " " _
                                    & "AND (COND_MIL_SCALE.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " ) " _
                                    & "ORDER BY DESCRIZIONE ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                Dim ds As New Data.DataSet()
                da.Fill(ds, "MILLESIMISCALE")

                DataGridScaleMill.DataSource = ds
                DataGridScaleMill.DataBind()

                Me.TxtTotScale.Text = ds.Tables(0).Rows.Count
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.btnVisualizza.Visible = True
                    Me.btnDelete.Visible = True
                Else
                    Me.btnVisualizza.Visible = False
                    Me.btnDelete.Visible = False
                End If

                da.Dispose()
                ds.Dispose()
                par.cmd.CommandText = "SELECT COND_EDIFICI.ID_EDIFICIO, EDIFICI.COD_EDIFICIO,EDIFICI.DENOMINAZIONE AS FABBRICATO , TO_CHAR(COND_EDIFICI.MIL_PROP_COND,'9G999G990D9999')AS MIL_PROP_COND,TO_CHAR((SELECT SUM(MIL_PRO) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE COND_UI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND UNITA_IMMOBILIARI.ID = COND_UI.ID_UI AND UNITA_IMMOBILIARI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO),'9G999G990D9999') AS PROP_COM,TO_CHAR((CASE WHEN MIL_PROP_COND = 0 THEN 0 ELSE((100*(SELECT SUM(MIL_PRO) FROM SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI WHERE COND_UI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND UNITA_IMMOBILIARI.ID = COND_UI.ID_UI AND UNITA_IMMOBILIARI.ID_EDIFICIO =COND_EDIFICI.ID_EDIFICIO))/MIL_PROP_COND)END),'9G999G990D9999')AS PERCENTPROP FROM SISCOM_MI.EDIFICI, SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " AND EDIFICI.ID = COND_EDIFICI.ID_EDIFICIO"
                Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ds2 As New Data.DataSet()
                da2.Fill(ds2, "MILLESIMISCALE")

                DataGridFabbMill.DataSource = ds2
                DataGridFabbMill.DataBind()

                'Me.TxtTotScale.Text = par.IfEmpty(Me.TxtTotScale.Text, 0) + ds2.Tables(0).Rows.Count
                If ds2.Tables(0).Rows.Count > 0 Then
                    Me.BtnVisualEdif.Visible = True
                Else
                    Me.BtnVisualEdif.Visible = False
                End If


                da2.Dispose()
                ds2.Dispose()

                par.cmd.CommandText = "select tot_scale from SISCOM_MI.CONDOMINI where id = " & CType(Me.Page, Object).vIdCondominio
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore(0), 0) > 0 And par.IfNull(lettore(0), 0) <> TxtTotScale.Text Then
                        Me.TxtTotScale.Text = lettore(0)
                    End If
                End If
                lettore.Close()

            Else
                Me.btnVisualizza.Visible = False
                Me.BtnVisualEdif.Visible = False

            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMillesimalil"
        End Try

    End Sub
    Public Sub CalcolaPercentuale()

        '*********PERCENTUALE SU MILLESIMI PROPRIETA'
        If Not String.IsNullOrEmpty(Me.txtMilPropComune.Text) AndAlso par.IfEmpty(Me.txtMilProp.Text, 0) > 0 Then
            Me.txtMilPropComunePerc.Text = Format(((Me.txtMilPropComune.Text * 100) / Me.txtMilProp.Text), "0.0000")
        End If
        '*********PERCENTUALE SU MILLESIMI COMPROPRIETA'
        If Not String.IsNullOrEmpty(Me.txtMillCompComune.Text) AndAlso par.IfEmpty(Me.txtMillComp.Text, 0) > 0 Then
            Me.txtMillCompComunePerc.Text = Format(((Me.txtMillCompComune.Text * 100) / Me.txtMillComp.Text), "0.0000")
        End If

        '*********PERCENTUALE SU MILLESIMI SUPERFICI
        If Not String.IsNullOrEmpty(Me.TxtMillSupComune.Text) AndAlso par.IfEmpty(Me.TxtMillSup.Text, 0) > 0 Then
            Me.TxtMillSupComunePerc.Text = Format(((Me.TxtMillSupComune.Text * 100) / Me.TxtMillSup.Text), "0.0000")
        End If
        '*********PERCENTUALE SU MILLESIMI GESTIONE
        If Not String.IsNullOrEmpty(Me.txtMillGestComune.Text) AndAlso par.IfEmpty(Me.txtMillGest.Text, 0) > 0 Then
            Me.txtMillGestComunePerc.Text = Format(((Me.txtMillGestComune.Text * 100) / Me.txtMillGest.Text), "0.0000")
        End If
        '*********PERCENTUALE SU MILLESIMI PRESENZA MODIFICA 05/01/2011
        If Not String.IsNullOrEmpty(Me.txtMillPresComune.Text) AndAlso par.IfEmpty(Me.txtMillPres.Text, 0) > 0 Then
            Me.txtMillPresComunePerc.Text = Format(((Me.txtMillPresComune.Text * 100) / Me.txtMillPres.Text), "0.0000")
        End If
        If DirectCast(Me.Page.FindControl("AggPercent"), HiddenField).Value = 0 Then
            '****Aggiorno il valore percentuale dei millesimi del tab convocazioni****
            ''LE PERCENTUALI DEL CONDOMINIO IN ESAME POSSONO CAMBIARE E VANNO AGGIORNATE!!!!
            CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtPercMilProp"), TextBox).Text = txtMilPropComunePerc.Text
            CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtpercSup"), TextBox).Text = TxtMillSupComunePerc.Text
            CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtPercMillComp"), TextBox).Text = txtMillCompComunePerc.Text
            CType(Me.Page.FindControl("TabConvocazione1").FindControl("txtPercMillPresAss"), TextBox).Text = txtMillPresComunePerc.Text

            ' ''****Aggiorno il valore percentuale nel tab millesimali****
        End If

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value <> "0" And DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value <> "" Then
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT COND_MIL_SCALE.*, SCALE_EDIFICI.DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.COND_MIL_SCALE WHERE ID_SCALA = " & DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value & " AND COND_MIL_SCALE.ID_SCALA = SCALE_EDIFICI.ID AND COND_MIL_SCALE.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtPropCondomominio.Text = IsNumFormat(myReader1("MIL_PROP_COND"), "", "0.0000")
                Me.txtMilScaleCondominio.Text = IsNumFormat(myReader1("MIL_ASC_COND"), "", "0.0000")
                Me.lblScala.Text = myReader1("DESCRIZIONE").ToString
            End If
            DirectCast(Me.Page.FindControl("TextBox3"), HiddenField).Value = 2
            DirectCast(Me.Page.FindControl("TextBox5"), HiddenField).Value = 1
            DirectCast(Me.Page.FindControl("txttab"), HiddenField).Value = 3
        Else
            DirectCast(Me.Page.FindControl("TextBox3"), HiddenField).Value = 1
        End If

    End Sub

    Protected Sub DataGridScaleMill_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridScaleMill.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabMillesimalil1_txtmia').value='Hai selezionato la Scala " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('idMillScale').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabMillesimalil1_txtmia').value='Hai selezionato la Scala " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('idMillScale').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MIL_SCALE SET MIL_ASC_COND = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMilScaleCondominio.Text), "Null") & ", MIL_PROP_COND = " & par.IfEmpty(par.VirgoleInPunti(Me.txtPropCondomominio.Text), "Null") & " WHERE ID_SCALA = " & DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value & " AND COND_MIL_SCALE.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()
        par.cmd.ExecuteNonQuery()
        '****************MYEVENT*****************
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI SCALE CONDOMINIO')"
        par.cmd.ExecuteNonQuery()


        DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value = 0
        Me.lblScala.Text = ""
        Me.txtMilScaleCondominio.Text = ""
        Me.txtPropCondomominio.Text = ""

        Me.txtmia.Text = "Nessuna Selezione"

        DirectCast(Me.Page.FindControl("TextBox3"), HiddenField).Value = 1
        CercaScale()
    End Sub

    Protected Sub DataGridFabbMill_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridFabbMill.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabMillesimalil1_txtmiaFabb').value='Hai selezionato il Fabbricato " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('idMillFabb').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabMillesimalil1_txtmiaFabb').value='Hai selezionato il Fabbricato " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('idMillFabb').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_EDIFICI SET MIL_PROP_COND = " & par.IfEmpty(par.VirgoleInPunti(Me.txtPropCondFabb.Text), "Null") & "WHERE ID_EDIFICIO = " & DirectCast(Me.Page.FindControl("idMillFabb"), HiddenField).Value & " AND ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()
        par.cmd.ExecuteNonQuery()
        '****************MYEVENT*****************
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI FABBRICATO PROPRIETA CONDOMINIO')"
        par.cmd.ExecuteNonQuery()


        DirectCast(Me.Page.FindControl("idMillFabb"), HiddenField).Value = 0
        Me.LblFabbricato.Text = ""
        Me.txtPropCondFabb.Text = ""

        Me.txtmiaFabb.Text = "Nessuna Selezione"

        DirectCast(Me.Page.FindControl("TextBox5"), HiddenField).Value = 1
        CercaScale()

    End Sub

    Protected Sub BtnVisualEdif_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnVisualEdif.Click
        If DirectCast(Me.Page.FindControl("idMillFabb"), HiddenField).Value <> "0" And DirectCast(Me.Page.FindControl("idMillFabb"), HiddenField).Value <> "" Then
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT COND_EDIFICI.*, EDIFICI.DENOMINAZIONE FROM SISCOM_MI.COND_EDIFICI, SISCOM_MI.EDIFICI WHERE ID_EDIFICIO = " & DirectCast(Me.Page.FindControl("idMillFabb"), HiddenField).Value & " AND COND_EDIFICI.ID_EDIFICIO = EDIFICI.ID AND COND_EDIFICI.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtPropCondFabb.Text = IsNumFormat(myReader1("MIL_PROP_COND"), "", "0.0000")
                Me.LblFabbricato.Text = myReader1("DENOMINAZIONE").ToString
            End If

            DirectCast(Me.Page.FindControl("TextBox5"), HiddenField).Value = 2
            DirectCast(Me.Page.FindControl("TextBox3"), HiddenField).Value = 1
            DirectCast(Me.Page.FindControl("txttab"), HiddenField).Value = 3

        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        If DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value <> "0" And DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value <> "" AndAlso Me.ConfElimina.Value = 1 Then
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MIL_SCALE SET FL_VISIBILE=0 WHERE ID_SCALA = " & DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value & " AND COND_MIL_SCALE.ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()
            par.cmd.ExecuteNonQuery()
            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','NASCOSTA SCALA')"
            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")

            DirectCast(Me.Page.FindControl("idMillScale"), HiddenField).Value = 0
            DirectCast(Me.Page.FindControl("txttab"), HiddenField).Value = 3
            DirectCast(Me.Page.FindControl("TextBox5"), HiddenField).Value = 1
            DirectCast(Me.Page.FindControl("TextBox3"), HiddenField).Value = 1

            Me.txtmia.Text = "Nessuna Selezione"
            CercaScale()
            ConfElimina.Value = 0
        Else
            DirectCast(Me.Page.FindControl("TextBox3"), HiddenField).Value = 1
            DirectCast(Me.Page.FindControl("TextBox5"), HiddenField).Value = 1
            ConfElimina.Value = 0
        End If

    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function
End Class
