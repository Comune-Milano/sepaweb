
Partial Class Contabilita_CicloPassivo_Plan_DividiImportoEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idVoce.Value = Request.QueryString("IDV")
            idLotto.Value = Request.QueryString("IDL")
            idServizio.Value = Request.QueryString("IDS")
            idPianoF.Value = Request.QueryString("IDP")
            IDVS.Value = Request.QueryString("IDVS")
            IDC.Value = Request.QueryString("IDC")
            IMP.Value = Request.QueryString("IMP")

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

            par.cmd.CommandText = "select sum(IMPORTO_CANONE_LORDO+IMPORTO_CONSUMO_LORDO) FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_LOTTO=" & idLotto.Value & " AND ID_VOCE_IMPORTO=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblDaDividere.Text = Format(lblImporto.Text - par.IfNull(myReader5(0), "0"), "##,##0.00")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'If lblDaDividere.Text < 0 Then
            '    Response.Write("alert('Attenzione...è stato diviso un importo superiore a quello disponibile!!\nDividere correttamente gli importi altrimenti il business plan non sarà approvato!');")
            'End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaGriglia()
        Dim i As Integer = 0
        Dim di As DataGridItem
        'Dim Importo As Double = 0
        Dim ImportoCanone As Double = 0
        Dim ImportoConsumo As Double = 0

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where lotti_patrimonio_importi.id_lotto=" & idLotto.Value & " and lotti_patrimonio_importi.id_voce_importo=" & IDVS.Value & " and id_complesso=" & IDC.Value & " and id_edificio is not null"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = False Then
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select edifici.id as id_edificio,edifici.cod_edificio,edifici.denominazione||case when condominio=1 then '<span class=''style1''> - (C)</span>' else '' end as DENOMINAZIONE,'0,00' AS IMPORTO_CANONE_LORDO,'0,00' AS IMPORTO_CONSUMO_LORDO from siscom_mi.edifici where id_complesso=" & IDC.Value & " order by denominazione asc", par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "tab_servizi_voci")
                DataGridVoci.DataSource = ds
                DataGridVoci.DataBind()

                For i = 0 To Me.DataGridVoci.Items.Count - 1
                    di = Me.DataGridVoci.Items(i)
                    'Importo = DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text
                    ImportoCanone = DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text
                    ImportoConsumo = DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text

                    If String.IsNullOrEmpty(ImportoCanone) = False Or String.IsNullOrEmpty(ImportoConsumo) = False Then

                        par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_edificio =" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_complesso=" & IDC.Value
                        par.cmd.ExecuteNonQuery()

                        'If Importo > 0 Then
                        par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_complesso,id_edificio,id_voce_importo,IMPORTO_CONSUMO_LORDO,IMPORTO_CANONE_LORDO) values (" & idLotto.Value & "," & IDC.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & "," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & ")"
                        par.cmd.ExecuteNonQuery()
                        'End If

                    End If
                Next

            Else

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select edifici.id as id_edificio,edifici.cod_edificio,edifici.denominazione||case when condominio=1 then '<span class=''style1''> - (C)</span>' else '' end as DENOMINAZIONE,trim(TO_CHAR(lotti_patrimonio_importi.importo_CANONE_LORDO,'9999990D99')) AS IMPORTO_CANONE_LORDO,trim(TO_CHAR(lotti_patrimonio_importi.importo_CONSUMO_LORDO,'9999990D99')) AS IMPORTO_CONSUMO_LORDO from siscom_mi.edifici,siscom_mi.lotti_patrimonio_importi where lotti_patrimonio_importi.id_scala is null and lotti_patrimonio_importi.id_lotto=" & idLotto.Value & " and edifici.id=lotti_patrimonio_importi.id_edificio and lotti_patrimonio_importi.id_voce_importo=" & IDVS.Value & " and lotti_patrimonio_importi.id_complesso=" & IDC.Value & " order by edifici.denominazione asc", par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "pf_voci_importo")
                DataGridVoci.DataSource = ds
                DataGridVoci.DataBind()
            End If
            myReader.Close()

            par.cmd.CommandText = "select SUM(importo_CONSUMO_LORDO+IMPORTO_CANONE_LORDO) AS IMPORTO from siscom_mi.lotti_patrimonio_importi where id_edificio is null and id_lotto=" & idLotto.Value & " and id_voce_importo=" & IDVS.Value & " and id_complesso=" & IDC.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("importo"), 0) > 0 Then
                    Response.Write("<script>alert('Attenzione...è stato inserito un importo a livello di complesso, quindi non è possibile inserire importi per i singoli edifici.\nSe si desidera inserire gli importi a livello di edifici, inserire 0,00 come importo per il complesso e salvare.');</script>")
                    ImgProcedi.Visible = False
                End If
            End If
            myReader1.Close()


            par.OracleConn.Close()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function


    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)
                CType(di.Cells(3).FindControl("txtImportoCanone"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(di.Cells(3).FindControl("txtImportoConsumo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Function CaricaVoce()
        Try


            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = myReader5("DESCRIZIONE")
                idLotto.Value = myReader5("ID_LOTTO")
                lblImporto.Text = Format((CDbl(par.IfNull(myReader5("VALORE_CANONE"), 0) + ((par.IfNull(myReader5("iva_CANONE"), 0) / 100) * par.IfNull(myReader5("VALORE_CANONE"), 0)))) + (CDbl(par.IfNull(myReader5("VALORE_CONSUMO"), 0) + ((par.IfNull(myReader5("iva_CONSUMO"), 0) / 100) * par.IfNull(myReader5("VALORE_CONSUMO"), 0)))), "##,##0.00")

            End If
            myReader5.Close()

            par.cmd.CommandText = "select * FROM SISCOM_MI.LOTTI WHERE ID=" & idLotto.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblLotto.Text = myReader5("DESCRIZIONE")
            End If
            myReader5.Close()

            par.cmd.CommandText = "select * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=" & IDC.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblComplesso.Text = par.IfNull(myReader5("COD_COMPLESSO"), "") & " - " & par.IfNull(myReader5("DENOMINAZIONE"), "")
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
            Dim buono As Boolean = True

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            Dim i As Integer = 0
            Dim di As DataGridItem
            ' Dim Importo As Double = 0
            Dim ImportoCanone As Double = 0
            Dim ImportoConsumo As Double = 0


            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)
                'Importo = par.IfEmpty(par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text, 0), "0,00")
                ImportoCanone = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text, "0,00")
                ImportoConsumo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text, "0,00")

                If String.IsNullOrEmpty(ImportoCanone) = False Or String.IsNullOrEmpty(ImportoConsumo) = False Then

                    par.cmd.CommandText = "delete from siscom_mi.lotti_patrimonio_importi where id_scala is null and id_voce_importo=" & IDVS.Value & " and id_lotto=" & idLotto.Value & " and id_edificio=" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & " and id_complesso=" & IDC.Value
                    par.cmd.ExecuteNonQuery()

                    'If Importo > 0 Then


                    par.cmd.CommandText = "insert into siscom_mi.lotti_patrimonio_importi (id_lotto,id_complesso,id_edificio,id_voce_importo,IMPORTO_CANONE_LORDO,IMPORTO_CONSUMO_LORDO) values (" & idLotto.Value & "," & IDC.Value & "," & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(0).Text) & "," & IDVS.Value & "," & par.IfEmpty(par.VirgoleInPunti(ImportoCanone), "null") & "," & par.IfEmpty(par.VirgoleInPunti(ImportoConsumo), "null") & ")"
                    par.cmd.ExecuteNonQuery()
               


                'End If

                End If
            Next

            Dim Importo As Double = 0
            Importo = 0

            par.cmd.CommandText = "select sum(importo_consumo_lordo+importo_canone_lordo) from siscom_mi.lotti_patrimonio_importi where id_lotto=" & idLotto.Value & " and id_voce_importo=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Importo = Importo + CDbl(par.IfNull(myReader5(0), 0)) 'par.VirgoleInPunti(par.IfNull(myReader5(0), 0))
            End If
            myReader5.Close()

            If Importo > CDbl(par.PuntiInVirgole(IMP.Value)) Then
                Response.Write("<script>alert('Gli importi inseriti, a livello di complesso e a livello di edificio, sono superiori rispetto all\'importo da dividere!\nSi ricorda che è possibile destinare un importo al complesso oppure agli edifici che compongono il complesso stesso o le scale che compongono l\'edificio!\nI dati saranno comunque salvati!');</script>")
                'buono = False
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

    Protected Sub DataGridVoci_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVoci.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         

            e.Item.Attributes.Add("onmouseover", "document.getElementById('indice1').value='" & e.Item.Cells(0).Text & "';")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub



    Protected Sub ImageDivisione_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        CaricaResto()
    End Sub

    Protected Sub DataGridVoci_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridVoci.SelectedIndexChanged

    End Sub
End Class
