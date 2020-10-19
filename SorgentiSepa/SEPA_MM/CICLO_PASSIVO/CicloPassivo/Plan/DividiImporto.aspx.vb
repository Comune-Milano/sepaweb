
Partial Class Contabilita_CicloPassivo_Plan_DividiImporto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public jj As String = "ss"

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property dtGriglia() As Data.DataTable
        Get
            If Not (ViewState("dtGriglia") Is Nothing) Then
                Return ViewState("dtGriglia")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtGriglia") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        'Dim Str As String


        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"
        'Response.Write(Str)




        If IsPostBack = False Then

            ' Response.Flush()
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

            lIdConnessione = "123"
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add(lIdConnessione, par.OracleConn)
            Session.Add("lIdConnessione", lIdConnessione)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE_1", par.myTrans)

            CaricaVoce()
            CaricaGriglia()

        End If

        AddJavascriptFunction()
        CaricaResto()

    End Sub


    Private Function CaricaResto()
        Try


            'par.OracleConn.Open()
            'par.SettaCommand(par)
            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE_1"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "select sum(importo_canone_lordo+importo_consumo_lordo) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblDaDividere.Text = Format(lblImporto.Text - par.IfNull(myReader5(0), "0"), "##,##0.00")
            End If
            myReader5.Close()

            

            par.cmd.Dispose()
            ' par.OracleConn.Close()
            ' Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



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
        dtGriglia = New Data.DataTable
        Dim ImportoCanone As Double = 0
        Dim ImportoConsumo As Double = 0

        Try
            ' par.OracleConn.Open()
            ' par.SettaCommand(par)

            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE_1"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            'If tipolotto.Value = "E" Then

            'lotti edifici
            par.cmd.CommandText = "select EDIFICI.id as id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(lotti_patrimonio_importi.importo_canone_lordo,'999999999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(lotti_patrimonio_importi.importo_CONSUMO_LORDO,'999999999990D99')) AS IMPORTO_CONSUMO_LORDO from siscom_mi.EDIFICI,siscom_mi.lotti_patrimonio_importi where lotti_patrimonio_importi.id_lotto=" & idLotto.Value & " and EDIFICI.id=lotti_patrimonio_importi.id_EDIFICIO and lotti_patrimonio_importi.id_voce_importo=" & IDVS.Value & "  order by EDIFICI.denominazione asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = False Then

                'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select complessi_immobiliari.id as id_complesso,complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,'0,00' as IMPORTO_CANONE_LORDO,'0,00' AS IMPORTO_CONSUMO_LORDO from siscom_mi.complessi_immobiliari, siscom_mi.lotti_patrimonio where complessi_immobiliari.id=lotti_patrimonio.id_complesso and lotti_patrimonio.id_lotto=" & idLotto.Value & " order by denominazione asc", par.OracleConn)
                par.cmd.CommandText = "select '0' AS TROVATO,EDIFICI.id as id_EDIFICIO,EDIFICI.cod_EDIFICIO,EDIFICI.denominazione||case when condominio=1 then '<span class=''style1''> - (C)</span>' else '' end as DENOMINAZIONE,'0,00' as IMPORTO_CANONE_LORDO,'0,00' AS IMPORTO_CONSUMO_LORDO from siscom_mi.EDIFICI, siscom_mi.lotti_patrimonio where EDIFICI.id=lotti_patrimonio.id_EDIFICIO and lotti_patrimonio.id_lotto=" & idLotto.Value & " order by denominazione asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ds As New Data.DataSet()
                da.Fill(ds, "tab_servizi_voci")
                DataGridVoci.DataSource = ds
                DataGridVoci.DataBind()

                dtGriglia = ds.Tables(0)

                'Session.Add("dsComp", ds) 

                For i = 0 To Me.DataGridVoci.Items.Count - 1
                    di = Me.DataGridVoci.Items(i)

                    ImportoCanone = DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text
                    ImportoConsumo = DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text

                    'DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Attributes.Add("ID", "-1")

                    di.Cells(9).Visible = False

                    If String.IsNullOrEmpty(ImportoCanone) = False Or String.IsNullOrEmpty(ImportoConsumo) = False Then


                        par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_EDIFICIO=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                        par.cmd.ExecuteNonQuery()

                        If ImportoCanone <> 0 Or ImportoConsumo <> 0 Then
                            'par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_complesso,id_edificio,id_voce_importo,IMPORTO_CONSUMO_LORDO,IMPORTO_CANONE_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & ",null," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ")"
                            par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,ID_EDIFICIO,id_voce_importo,IMPORTO_CONSUMO_LORDO,IMPORTO_CANONE_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & "," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If

                    End If
                Next

            Else

                Dim ssql As String = "SELECT (SELECT COUNT(*) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_EDIFICIO=EDIFICI.ID AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_LOTTO=" & idLotto.Value & " and id_scala is not null) " _
                                   & "  AS TROVATO,EDIFICI.ID AS id_EDIFICIO,'<a href=""javascript:window.open(''../../../SPESE_REVERSIBILI/RisultatiModificaManuale.aspx?EDIFICIO='||EDIFICI.ID||'&CIVICO=-1&COMPLESSO=-1&INDIRIZZO=-1&INTERNO=-1&SCALA=-1&ASCENSORE=-1&TIPOLOGIA=&PLAN=1'',''_blank'',''resizable=1,statusbar=0'');void(0);"">'||EDIFICI.cod_EDIFICIO||'</a>' AS COD_EDIFICIO,EDIFICI.denominazione||case when condominio=1 then '<span class=''style1''> - (C)</span>' else '' end AS DENOMINAZIONE,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CANONE_LORDO,'999999999990D99')) AS IMPORTO_CANONE_LORDO," _
                                   & "trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_CONSUMO_LORDO,'999999999990D99')) AS IMPORTO_CONSUMO_LORDO " _
                                   & "FROM siscom_mi.EDIFICI,siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND LOTTI_PATRIMONIO_IMPORTI.id_COMPLESSO IS NULL AND " _
                                   & "LOTTI_PATRIMONIO_IMPORTI.id_lotto = " & idLotto.Value & " And EDIFICI.ID = LOTTI_PATRIMONIO_IMPORTI.id_EDIFICIO And LOTTI_PATRIMONIO_IMPORTI.id_voce_importo = " & IDVS.Value _
                                   & " UNION SELECT (SELECT COUNT(*) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_EDIFICIO=EDIFICI.ID AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_LOTTO=" & idLotto.Value & " AND id_scala IS NOT NULL) AS TROVATO," _
                                   & " EDIFICI.ID AS id_EDIFICIO,'<a href=""javascript:window.open(''../../../SPESE_REVERSIBILI/RisultatiModificaManuale.aspx?EDIFICIO='||EDIFICI.ID||'&CIVICO=-1&COMPLESSO=-1&INDIRIZZO=-1&INTERNO=-1&SCALA=-1&ASCENSORE=-1&TIPOLOGIA=&PLAN=1'',''_blank'',''resizable=1,statusbar=0'');void(0);"">'||EDIFICI.cod_EDIFICIO||'</a>' AS COD_EDIFICIO,EDIFICI.denominazione,trim(TO_CHAR(NVL((SELECT SUM(IMPORTO_CANONE_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NOT NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_EDIFICIO=EDIFICI.ID),0),'999999999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(NVL((SELECT SUM(IMPORTO_CONSUMO_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NOT NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & " AND ID_EDIFICIO=EDIFICI.ID),0),'999999999990D99')) AS IMPORTO_CONSUMO_LORDO " _
                                   & " FROM siscom_mi.EDIFICI, siscom_mi.LOTTI_PATRIMONIO WHERE " _
                                   & "EDIFICI.ID=LOTTI_PATRIMONIO.id_EDIFICIO AND LOTTI_PATRIMONIO.id_lotto=" & idLotto.Value & " AND (id_lotto,id_EDIFICIO) NOT IN (SELECT id_lotto,id_EDIFICIO FROM siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE ID_SCALA IS NULL AND ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value & ") " _
                                   & "ORDER BY denominazione ASC"


                par.cmd.CommandText = ssql
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(ssql, par.OracleConn)





                Dim ds As New Data.DataSet()
                da.Fill(ds, "pf_voci_importo")
                DataGridVoci.DataSource = ds
                DataGridVoci.DataBind()

                dtGriglia = ds.Tables(0)
                'Session.Add("dsComp", ds)

                For i = 0 To Me.DataGridVoci.Items.Count - 1
                    di = Me.DataGridVoci.Items(i)
                    If Me.DataGridVoci.Items(i).Cells(1).Text <> "0" Then
                        di.Cells(9).Visible = True
                        DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Enabled = False
                        DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Enabled = False
                    Else
                        di.Cells(9).Visible = False
                    End If
                    'di = Me.DataGridVoci.Items(i)
                    'If Me.DataGridVoci.Items(i).Cells(0).Text <> "-1" Then
                    '    par.cmd.CommandText = "select COUNT(*) from siscom_mi.lotti_patrimonio_importi where id_edificio=" & Me.DataGridVoci.Items(i).Cells(0).Text & " and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_scala is not null"
                    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '    If myReader1.HasRows = True Then
                    '        di.Cells(8).Visible = True
                    '        DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Enabled = False
                    '        DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Enabled = False
                    '    Else
                    '        di.Cells(8).Visible = False
                    '    End If
                    '    myReader1.Close()
                    'End If

                Next

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



            ' par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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

            ' par.OracleConn.Open()
            ' par.SettaCommand(par)

            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE_1"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

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
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        SalvaDati()
    End Sub

    Private Function SalvaDati1()
        Try

            If PROVENIENZAda.Value = "1" Then

                'ricarico i valori modificati nella griglia
                Dim indice2 As Integer = 0
                For indice As Integer = DataGridVoci.CurrentPageIndex * DataGridVoci.PageSize To DataGridVoci.CurrentPageIndex * DataGridVoci.PageSize + Math.Min(DataGridVoci.PageSize - 1, DataGridVoci.Items.Count - 1)
                    dtGriglia.Rows(indice).Item(4) = CStr(CDec(CType(DataGridVoci.Items(indice2).FindControl("txtImportoCanone"), TextBox).Text))
                    indice2 += 1
                Next


                '    Try
                '        'provenienza da preventivi, controllare che la somma degli importi divisi sia pari al totale, altrimenti alert di avviso
                '        Dim datagriditem As DataGridItem
                '        Dim importoCanoneTotale As Decimal = 0
                '        For index As Integer = 0 To Me.DataGridVoci.Items.Count - 1
                '            datagriditem = Me.DataGridVoci.Items(index)
                '            importoCanoneTotale += CDec(par.IfEmpty(DirectCast(datagriditem.Cells(1).FindControl("txtImportoCanone"), TextBox).Text, "0,00"))
                '        Next

                '        Dim importodadividere As Decimal = CDec(lblImporto.Text)
                '        If importodadividere <> importoCanoneTotale Then
                '            Response.Write("<script>alert('Gli importi inseriti non coincidono con l\'importo da dividere!');</script>")
                '            Exit Function
                '        End If
                '        'usciamo dalla sub e non fa nessun tipo di modifica
                '    Catch ex As Exception
                '        par.cmd.Dispose()
                '        par.OracleConn.Close()
                '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '    End Try

            End If

            Dim buono As Boolean = True



            'par.OracleConn.Open()
            'par.SettaCommand(par)
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans
            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE_1"), Oracle.DataAccess.Client.OracleTransaction)
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

                            If ImportoCanone = 0 And ImportoConsumo = 0 Then
                                par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_lotto=" & idLotto.Value & " and id_complesso IS NULL and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderT.HasRows = True Then
                                    par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_COMPLESSO is null and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_EDIFICIO=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                                    par.cmd.ExecuteNonQuery()
                                End If
                                myReaderT.Close()

                            End If

                            If ImportoCanone <> 0 Or ImportoConsumo <> 0 Then
                                par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_lotto=" & idLotto.Value & " and id_complesso IS NULL and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderT.HasRows = True Then
                                    par.cmd.CommandText = "update siscom_mi.lotti_patrimonio_importi set IMPORTO_CANONE_LORDO=" & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ",IMPORTO_CONSUMO_LORDO=" & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & " where id_lotto=" & idLotto.Value & " and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_EDIFICIO,id_COMPLESSO,id_voce_importo,IMPORTO_CANONE_LORDO,IMPORTO_CONSUMO_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & ",null," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                                myReaderT.Close()
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



            ' par.myTrans.Commit()
            ' par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If buono = True Then
                'Response.Write("<script>alert('Operazione Effettuata!');</script>")
                'txtmodificato.Value = "0"
            Else
                'Response.Write("<script>alert('Operazione NON Effettuata!');</script>")
            End If

            CaricaResto()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Private Function SalvaDati()
        Try

            If PROVENIENZAda.Value = "1" Then
                Try
                    'provenienza da preventivi, controllare che la somma degli importi divisi sia pari al totale, altrimenti alert di avviso

                    'ricarico i valori modificati nella griglia
                    Dim indice2 As Integer = 0
                    For indice As Integer = DataGridVoci.CurrentPageIndex * DataGridVoci.PageSize To DataGridVoci.CurrentPageIndex * DataGridVoci.PageSize + Math.Min(DataGridVoci.PageSize - 1, DataGridVoci.Items.Count - 1)
                        dtGriglia.Rows(indice).Item(4) = CStr(CDec(CType(DataGridVoci.Items(indice2).FindControl("txtImportoCanone"), TextBox).Text))
                        indice2 += 1
                    Next


                    'controllo se gli importi della griglia coincidono col totale impostato
                    Dim dt As Data.DataTable = dtGriglia
                    Dim dtrow As Data.DataRow
                    Dim importoCanoneTotale As Decimal = 0
                    For index As Integer = 0 To dt.Rows.Count - 1
                        dtrow = dt.Rows(index)
                        importoCanoneTotale += CDec(dtrow.Item(4))
                    Next

                    Dim importodadividere As Decimal = CDec(lblImporto.Text)
                    If importodadividere <> importoCanoneTotale Then
                        Response.Write("<script>alert('Gli importi inseriti non coincidono con l\'importo da dividere!');</script>")
                        Exit Function
                    End If
                    'usciamo dalla sub e non fa nessun tipo di modifica
                Catch ex As Exception
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try

            End If

            Dim buono As Boolean = True



            'par.OracleConn.Open()
            'par.SettaCommand(par)
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans

            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE_1"), Oracle.DataAccess.Client.OracleTransaction)
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

                            If ImportoCanone = 0 And ImportoConsumo = 0 Then
                                par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_lotto=" & idLotto.Value & " and id_complesso IS NULL and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderT.HasRows = True Then
                                    par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_COMPLESSO is null and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_EDIFICIO=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text)
                                    par.cmd.ExecuteNonQuery()
                                End If
                                myReaderT.Close()

                            End If

                            If ImportoCanone <> 0 Or ImportoConsumo <> 0 Then
                                par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_lotto=" & idLotto.Value & " and id_complesso IS NULL and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderT.HasRows = True Then
                                    'par.cmd.CommandText = "update siscom_mi.lotti_patrimonio_importi set IMPORTO_CANONE_LORDO=" & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ",IMPORTO_CONSUMO_LORDO=" & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & " where id_lotto=" & idLotto.Value & " and id_complesso IS NULL and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                    par.cmd.CommandText = "update siscom_mi.lotti_patrimonio_importi set IMPORTO_CANONE_LORDO=" & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ",IMPORTO_CONSUMO_LORDO=" & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & " where id_lotto=" & idLotto.Value & " and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_EDIFICIO,id_COMPLESSO,id_voce_importo,IMPORTO_CANONE_LORDO,IMPORTO_CONSUMO_LORDO) values (" & idLotto.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & ",null," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                                myReaderT.Close()
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
            par.cmd.Dispose()

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE_1", par.myTrans)


            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
    End Function

    Protected Sub ImageDivisione_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        CaricaResto()
    End Sub

    Protected Sub DataGridVoci_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVoci.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "document.getElementById('indice').value='" & e.Item.Cells(0).Text & "';")
            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub DataGridVoci_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridVoci.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            SalvaDati1()
            DataGridVoci.CurrentPageIndex = e.NewPageIndex
            'DataGridVoci.DataSource = Session.Item("dsComp")
            'DataGridVoci.DataBind()
            CaricaGriglia()
        End If
    End Sub



    Protected Sub imgRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRefresh.Click
        CaricaGriglia()
    End Sub


    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalvaN.Click

    'End Sub

    Protected Sub imgSalvaN_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalvaN.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove(lIdConnessione)
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
    End Sub

End Class
