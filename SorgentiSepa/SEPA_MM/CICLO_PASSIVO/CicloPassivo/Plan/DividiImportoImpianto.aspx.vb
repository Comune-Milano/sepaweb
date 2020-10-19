
Partial Class Contabilita_CicloPassivo_Plan_DividiImporto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public jj As String = "ss"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            If Request.QueryString("PROV") = "PREV" Then
                PROVENIENZAda.Value = "1"
            End If
            idVoce.Value = Request.QueryString("IDV")
            idLotto.Value = Request.QueryString("IDL")
            idServizio.Value = Request.QueryString("IDS")
            idPianoF.Value = Request.QueryString("IDP")
            IDVS.Value = Request.QueryString("IDVS")
            'tipolotto.Value = Request.QueryString("T")
            'idimpianto.value = Request.QueryString("IDI")
            CaricaVoce()
            CaricaGriglia()

        End If

        AddJavascriptFunction()
        CaricaResto()

    End Sub


    Private Function CaricaResto()
        Try


            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select sum(importo_canone_lordo+importo_consumo_lordo) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblDaDividere.Text = Format(lblImporto.Text - par.IfNull(myReader5(0), "0"), "##,##0.00")
            End If
            myReader5.Close()

            

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)

                ' CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(di.Cells(3).FindControl("txtImportoCanone"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(di.Cells(3).FindControl("txtImportoConsumo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Function CaricaGriglia()
        Dim i As Integer = 0
        Dim di As DataGridItem

        Dim ImportoCanone As Double = 0
        Dim ImportoConsumo As Double = 0

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            'If tipolotto.Value = "E" Then

            'lotti edifici
            'par.cmd.CommandText = "select EDIFICI.id as id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(lotti_patrimonio_importi.importo_canone_lordo,'9999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(lotti_patrimonio_importi.importo_CONSUMO_LORDO,'9999990D99')) AS IMPORTO_CONSUMO_LORDO from siscom_mi.EDIFICI,siscom_mi.lotti_patrimonio_importi where lotti_patrimonio_importi.id_lotto=" & idLotto.Value & " and EDIFICI.id=lotti_patrimonio_importi.id_EDIFICIO and lotti_patrimonio_importi.id_voce_importo=" & IDVS.Value & "  order by EDIFICI.denominazione asc"

            par.cmd.CommandText = "SELECT IMPIANTI.ID AS ID_IMPIANTO,IMPIANTI.COD_IMPIANTO,IMPIANTI.DESCRIZIONE AS DENOMINAZIONE,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_canone_lordo,'999999999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CONSUMO_LORDO,'999999999990D99')) AS IMPORTO_CONSUMO_LORDO FROM siscom_mi.IMPIANTI,siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE LOTTI_PATRIMONIO_IMPORTI.id_lotto=" & idLotto.Value & " AND IMPIANTI.ID=LOTTI_PATRIMONIO_IMPORTI.ID_IMPIANTO AND LOTTI_PATRIMONIO_IMPORTI.id_voce_importo=" & IDVS.Value & "  ORDER BY IMPIANTI.DESCRIZIONE ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = False Then

                'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select complessi_immobiliari.id as id_complesso,complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,'0,00' as IMPORTO_CANONE_LORDO,'0,00' AS IMPORTO_CONSUMO_LORDO from siscom_mi.complessi_immobiliari, siscom_mi.lotti_patrimonio where complessi_immobiliari.id=lotti_patrimonio.id_complesso and lotti_patrimonio.id_lotto=" & idLotto.Value & " order by denominazione asc", par.OracleConn)
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select IMPIANTI.id as ID_IMPIANTO,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''TrovaImpianto.aspx?ID='||IMPIANTI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=800'');£>'||IMPIANTI.COD_IMPIANTO||'</a>','$','&'),'£','" & Chr(34) & "') AS COD_IMPIANTO,IMPIANTI.DESCRIZIONE AS DENOMINAZIONE,'0,00' as IMPORTO_CANONE_LORDO,'0,00' AS IMPORTO_CONSUMO_LORDO from siscom_mi.IMPIANTI, siscom_mi.lotti_patrimonio where IMPIANTI.id=lotti_patrimonio.id_IMPIANTO and lotti_patrimonio.id_lotto=" & idLotto.Value & " order by denominazione asc", par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "tab_servizi_voci")
                DataGridVoci.DataSource = ds
                DataGridVoci.DataBind()


                For i = 0 To Me.DataGridVoci.Items.Count - 1
                    di = Me.DataGridVoci.Items(i)

                    ImportoCanone = DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text
                    ImportoConsumo = DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text

                    'di.Cells(8).Visible = False

                    If String.IsNullOrEmpty(ImportoCanone) = False Or String.IsNullOrEmpty(ImportoConsumo) = False Then

                        'par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_edificio is null and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_complesso=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                        par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_IMPIANTO=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                        par.cmd.ExecuteNonQuery()

                        If ImportoCanone <> 0 Or ImportoConsumo <> 0 Then
                            'par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_complesso,id_edificio,id_voce_importo,IMPORTO_CONSUMO_LORDO,IMPORTO_CANONE_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & ",null," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ")"
                            par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,ID_IMPIANTO,id_voce_importo,IMPORTO_CONSUMO_LORDO,IMPORTO_CANONE_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & "," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If

                    End If
                Next

            Else



                'Dim ssql As String = "SELECT EDIFICI.ID AS id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CANONE_LORDO,'9999990D99')) AS IMPORTO_CANONE_LORDO," _
                '                   & "trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CONSUMO_LORDO,'9999990D99')) AS IMPORTO_CONSUMO_LORDO " _
                '                   & "FROM siscom_mi.EDIFICI,siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND LOTTI_PATRIMONIO_IMPORTI.id_COMPLESSO IS NULL AND " _
                '                   & "LOTTI_PATRIMONIO_IMPORTI.id_lotto = " & idLotto.Value & " And EDIFICI.ID = LOTTI_PATRIMONIO_IMPORTI.id_EDIFICIO And LOTTI_PATRIMONIO_IMPORTI.id_voce_importo = " & IDVS.Value _
                '                   & " UNION SELECT EDIFICI.ID AS id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(NVL((SELECT SUM(IMPORTO_CANONE_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NOT NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_EDIFICIO=EDIFICI.ID),0),'9999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(NVL((SELECT SUM(IMPORTO_CONSUMO_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NOT NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_EDIFICIO=EDIFICI.ID),0),'9999990D99')) AS IMPORTO_CONSUMO_LORDO " _
                '                   & " FROM siscom_mi.EDIFICI, siscom_mi.LOTTI_PATRIMONIO WHERE " _
                '                   & "EDIFICI.ID=LOTTI_PATRIMONIO.id_EDIFICIO AND LOTTI_PATRIMONIO.id_lotto=" & idLotto.Value & " AND (id_lotto,id_EDIFICIO) NOT IN (SELECT id_lotto,id_EDIFICIO FROM siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & ") " _
                '                   & "ORDER BY denominazione ASC"

                Dim SSQL As String = "SELECT IMPIANTI.ID AS ID_IMPIANTO,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''TrovaImpianto.aspx?ID='||IMPIANTI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=800'');£>'||IMPIANTI.COD_IMPIANTO||'</a>','$','&'),'£','" & Chr(34) & "') AS COD_IMPIANTO,IMPIANTI.DESCRIZIONE AS denominazione,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CANONE_LORDO,'999999999990D99')) AS IMPORTO_CANONE_LORDO," _
                                   & " trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CONSUMO_LORDO,'999999999990D99')) AS IMPORTO_CONSUMO_LORDO FROM siscom_mi.IMPIANTI,siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE LOTTI_PATRIMONIO_IMPORTI.ID_EDIFICIO IS NULL " _
                                   & " AND ID_SCALA IS NULL AND LOTTI_PATRIMONIO_IMPORTI.id_lotto = " & idLotto.Value & " AND IMPIANTI.ID = LOTTI_PATRIMONIO_IMPORTI.ID_IMPIANTO AND " _
                                   & " LOTTI_PATRIMONIO_IMPORTI.id_voce_importo = " & IDVS.Value _
                                   & " UNION " _
                                   & " SELECT IMPIANTI.ID AS ID_IMPIANTO,IMPIANTI.COD_IMPIANTO,IMPIANTI.DESCRIZIONE AS denominazione,'' AS IMPORTO_CANONE_LORDO,'' AS IMPORTO_CONSUMO_LORDO  " _
                                   & " FROM siscom_mi.IMPIANTI, siscom_mi.LOTTI_PATRIMONIO WHERE IMPIANTI.ID=LOTTI_PATRIMONIO.id_IMPIANTO AND LOTTI_PATRIMONIO.id_lotto=" & idLotto.Value _
                                   & " AND (id_lotto,id_IMPIANTO) NOT IN (SELECT id_lotto,id_IMPIANTO FROM siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND ID_EDIFICIO IS NULL AND ID_LOTTO=" _
                                   & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & ") ORDER BY denominazione ASC"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(SSQL, par.OracleConn)





                Dim ds As New Data.DataSet()
                da.Fill(ds, "pf_voci_importo")
                DataGridVoci.DataSource = ds
                DataGridVoci.DataBind()

                'For i = 0 To Me.DataGridVoci.Items.Count - 1
                '    di = Me.DataGridVoci.Items(i)
                '    If Me.DataGridVoci.Items(i).Cells(0).Text <> "-1" Then
                '        par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_IMPIANTO=" & Me.DataGridVoci.Items(i).Cells(0).Text & " and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_I is not null"
                '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '        If myReader1.HasRows = True Then
                '            di.Cells(8).Visible = True
                '            DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Enabled = False
                '            DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Enabled = False
                '        Else
                '            di.Cells(8).Visible = False
                '        End If
                '        myReader1.Close()
                '    End If

                'Next

            End If
            myReader.Close()

            'Else

            ''lotti impianti

            'par.cmd.CommandText = "select EDIFICI.id as id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(lotti_patrimonio_importi.importo_canone_lordo,'9999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(lotti_patrimonio_importi.importo_CONSUMO_LORDO,'9999990D99')) AS IMPORTO_CONSUMO_LORDO from siscom_mi.EDIFICI,siscom_mi.lotti_patrimonio_importi where lotti_patrimonio_importi.id_lotto=" & idLotto.Value & " and EDIFICI.id=lotti_patrimonio_importi.id_EDIFICIO and lotti_patrimonio_importi.id_voce_importo=" & IDVS.Value & "  order by EDIFICI.denominazione asc"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.HasRows = False Then


            '    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select EDIFICI.id as id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,'0,00' as IMPORTO_CANONE_LORDO,'0,00' AS IMPORTO_CONSUMO_LORDO from siscom_mi.EDIFICI, siscom_mi.lotti_patrimonio where EDIFICI.id=lotti_patrimonio.id_EDIFICIO and lotti_patrimonio.id_lotto=" & idLotto.Value & " order by denominazione asc", par.OracleConn)
            '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT EDIFICI.ID AS id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,'0,00' AS IMPORTO_CANONE_LORDO, '0,00' AS IMPORTO_CONSUMO_LORDO FROM siscom_mi.EDIFICI, siscom_mi.LOTTI_PATRIMONIO WHERE EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.IMPIANTI WHERE ID=LOTTI_PATRIMONIO.id_IMPIANTO AND  LOTTI_PATRIMONIO.id_lotto=" & idLotto.Value & ") ORDER BY denominazione ASC", par.OracleConn)


            '    Dim ds As New Data.DataSet()
            '    da.Fill(ds, "tab_servizi_voci")
            '    DataGridVoci.DataSource = ds
            '    DataGridVoci.DataBind()


            '    For i = 0 To Me.DataGridVoci.Items.Count - 1
            '        di = Me.DataGridVoci.Items(i)

            '        ImportoCanone = DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text
            '        ImportoConsumo = DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text

            '        di.Cells(8).Visible = False

            '        If String.IsNullOrEmpty(ImportoCanone) = False Or String.IsNullOrEmpty(ImportoConsumo) = False Then


            '            par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_EDIFICIO=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
            '            par.cmd.ExecuteNonQuery()

            '            If ImportoCanone <> 0 Or ImportoConsumo <> 0 Then

            '                par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,ID_EDIFICIO,id_voce_importo,IMPORTO_CONSUMO_LORDO,IMPORTO_CANONE_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & "," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ")"
            '                par.cmd.ExecuteNonQuery()
            '            End If

            '        End If
            '    Next

            'Else

            '    Dim ssql As String = "SELECT EDIFICI.ID AS id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CANONE_LORDO,'9999990D99')) AS IMPORTO_CANONE_LORDO," _
            '                       & "trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CONSUMO_LORDO,'9999990D99')) AS IMPORTO_CONSUMO_LORDO " _
            '                       & "FROM siscom_mi.EDIFICI,siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND LOTTI_PATRIMONIO_IMPORTI.id_COMPLESSO IS NULL AND " _
            '                       & "LOTTI_PATRIMONIO_IMPORTI.id_lotto = " & idLotto.Value & " And EDIFICI.ID = LOTTI_PATRIMONIO_IMPORTI.id_EDIFICIO And LOTTI_PATRIMONIO_IMPORTI.id_voce_importo = " & IDVS.Value _
            '                       & " UNION SELECT EDIFICI.ID AS id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(NVL((SELECT SUM(IMPORTO_CANONE_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NOT NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_EDIFICIO=EDIFICI.ID),0),'9999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(NVL((SELECT SUM(IMPORTO_CONSUMO_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NOT NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_EDIFICIO=EDIFICI.ID),0),'9999990D99')) AS IMPORTO_CONSUMO_LORDO " _
            '                       & " FROM siscom_mi.EDIFICI, siscom_mi.LOTTI_PATRIMONIO WHERE " _
            '                       & "EDIFICI.ID=LOTTI_PATRIMONIO.id_EDIFICIO AND LOTTI_PATRIMONIO.id_lotto=" & idLotto.Value & " AND (id_lotto,id_EDIFICIO) NOT IN (SELECT id_lotto,id_EDIFICIO FROM siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & ") " _
            '                       & "ORDER BY denominazione ASC"

            '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(ssql, par.OracleConn)





            '    Dim ds As New Data.DataSet()
            '    da.Fill(ds, "pf_voci_importo")
            '    DataGridVoci.DataSource = ds
            '    DataGridVoci.DataBind()

            '    For i = 0 To Me.DataGridVoci.Items.Count - 1
            '        di = Me.DataGridVoci.Items(i)
            '        If Me.DataGridVoci.Items(i).Cells(0).Text <> "-1" Then
            '            par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_edificio=" & Me.DataGridVoci.Items(i).Cells(0).Text & " and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_scala is not null"
            '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '            If myReader1.HasRows = True Then
            '                di.Cells(8).Visible = True
            '                DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Enabled = False
            '                DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Enabled = False
            '            Else
            '                di.Cells(8).Visible = False
            '            End If
            '            myReader1.Close()
            '        End If

            '    Next

            'End If
            'myReader.Close()
            'End If



            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function



    Function CaricaVoce()
        Try

            Dim V1 As Double = 0
            Dim V2 As Double = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = par.IfNull(myReader5("DESCRIZIONE"), "")
                V1 = CDbl(Format((CDbl(par.IfNull(myReader5("VALORE_CANONE"), 0) + ((par.IfNull(myReader5("iva_CANONE"), 0) / 100) * par.IfNull(myReader5("VALORE_CANONE"), 0)))), "0.00"))
                V2 = CDbl(Format((CDbl(par.IfNull(myReader5("VALORE_CONSUMO"), 0) + ((par.IfNull(myReader5("iva_CONSUMO"), 0) / 100) * par.IfNull(myReader5("VALORE_CONSUMO"), 0)))), "0.00"))
                lblImporto.Text = Format(V1 + V2, "##,##0.00")

                importovoce.Value = par.VirgoleInPunti(V1 + V2)
                idLotto.Value = myReader5("ID_LOTTO")
            End If
            myReader5.Close()

            par.cmd.CommandText = "select * FROM SISCOM_MI.LOTTI WHERE ID=" & idLotto.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblLotto.Text = myReader5("DESCRIZIONE")
            End If
            myReader5.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try

            If PROVENIENZAda.Value = "1" Then
                Try
                    'provenienza da preventivi, controllare che la somma degli importi divisi sia pari al totale, altrimenti alert di avviso
                    Dim datagriditem As DataGridItem
                    Dim importoCanoneTotale As Decimal = 0
                    For index As Integer = 0 To Me.DataGridVoci.Items.Count - 1
                        datagriditem = Me.DataGridVoci.Items(index)
                        importoCanoneTotale += CDec(par.IfEmpty(DirectCast(datagriditem.Cells(1).FindControl("txtImportoCanone"), TextBox).Text, "0,00"))
                    Next

                    Dim importodadividere As Decimal = CDec(lblImporto.Text)
                    If importodadividere <> importoCanoneTotale Then
                        Response.Write("<script>alert('Gli importi inseriti non coincidono con l\'importo da dividere!');</script>")
                        Exit Sub
                    End If
                    'usciamo dalla sub e non fa nessun tipo di modifica
                Catch ex As Exception
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try

            End If


            Dim buono As Boolean = True

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            Dim i As Integer = 0
            Dim di As DataGridItem

            Dim ImportoCanone As Double = 0
            Dim ImportoConsumo As Double = 0

            If buono = True Then
                For i = 0 To Me.DataGridVoci.Items.Count - 1
                    di = Me.DataGridVoci.Items(i)
                    'Importo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text, "0,00")
                    ImportoCanone = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text, "0,00")
                    ImportoConsumo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text, "0,00")

                    If DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Enabled = True Then
                        If String.IsNullOrEmpty(ImportoCanone) = False Or String.IsNullOrEmpty(ImportoConsumo) = False Then

                            'par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_edificio is null and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_complesso=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                            par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_IMPIANTO=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                            par.cmd.ExecuteNonQuery()

                            If ImportoCanone <> 0 Or ImportoConsumo <> 0 Then
                                'par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_complesso,id_edificio,id_voce_importo,IMPORTO_CANONE_LORDO,IMPORTO_CONSUMO_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & ",null," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & ")"
                                par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_IMPIANTO,id_voce_importo,IMPORTO_CANONE_LORDO,IMPORTO_CONSUMO_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & "," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & ")"
                                par.cmd.ExecuteNonQuery()
                            End If

                        End If
                    End If
                Next
            End If

            Dim Importo As Double = 0

            Importo = 0
            par.cmd.CommandText = "select sum(importo_consumo_lordo+importo_canone_lordo) from siscom_mi.lotti_patrimonio_importi where id_lotto=" & idLotto.Value & " and id_voce_importo=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Importo = Importo + CDbl(par.IfNull(myReader5(0), 0))
            End If
            myReader5.Close()

            If Importo <> CDbl(lblImporto.Text) Then
                Response.Write("<script>alert('Gli importi inseriti, a livello di complesso e a livello di edificio, non coincidono con l\'importo da dividere!\nSi ricorda che è possibile destinare un importo al complesso oppure agli edifici che compongono il complesso stesso!\nI dati saranno comunque salvati!');</script>")
            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If buono = True Then
                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                txtmodificato.Value = "0"
            Else
                Response.Write("<script>alert('Operazione NON Effettuata!');</script>")
            End If

            CaricaResto()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ImageDivisione_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        CaricaResto()
    End Sub

    Protected Sub DataGridVoci_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVoci.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
      
            e.Item.Attributes.Add("onmouseover", "document.getElementById('indice').value='" & e.Item.Cells(0).Text & "';")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub DataGridVoci_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridVoci.SelectedIndexChanged

    End Sub


End Class
