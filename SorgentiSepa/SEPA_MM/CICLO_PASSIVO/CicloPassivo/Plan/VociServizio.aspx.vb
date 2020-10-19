
Partial Class Contabilita_CicloPassivo_Plan_VociServizio
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsNothing(Request.QueryString("PROV")) Then
            If Request.QueryString("PROV") = "PREVENTIVI" Then
                ImgProcedi0.Visible = False
                PROVENIENZAda.Value = "1"
            End If
        End If

        Response.Expires = 0

        If IsPostBack = False Then
            idVoce.Value = Request.QueryString("IDV")
            idLotto.Value = Request.QueryString("IDL")
            idServizio.Value = Request.QueryString("IDS")
            idPianoF.Value = Request.QueryString("IDP")
            tipolotto.Value = Request.QueryString("T")
            If Request.QueryString("PR") = "1" Then
                ImgProcedi0.Visible = False
                TotaleLordoRichiesto.Visible = True
                lblApprovato.Visible = True
                TotaleLordoRichiesto.Text = Format(CDbl(par.DeCripta(Request.QueryString("APP"))), "##,##0.00")
            End If

            CaricaValori()
            CaricaGriglia()
            CaricaLordo()




        End If
        AddJavascriptFunction()
       




    End Sub

    Public Function CaricaGriglia()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim reve As String = "0"
            If reversibilita.Value = "0" Then
                reve = "=0"
            Else
                reve = ">0"
            End If


            If tipolotto.Value = "E" Then

                par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & idVoce.Value & " and id_lotto=" & idLotto.Value & " AND ID_SERVIZIO=" & idServizio.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = False Then
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TAB_SERVIZI_VOCI.ID AS IDVOCESERVIZIO,'0,00' as IMPORTOCONSUMO,'0,00' as IMPORTO,'-1' as id,tab_servizi_voci.descrizione,tab_servizi_voci.iva_CANONE as iva, tab_servizi_voci.iva_consumo as ivaconsumo, tab_servizi_voci.perc_reversibilita from siscom_mi.tab_servizi_voci where  TAB_SERVIZI_VOCI.ID_VOCE=" & idVoce.Value & " AND  tab_servizi_voci.id_servizio=" & idServizio.Value & " and tab_servizi_voci.perc_reversibilita" & reve & " order by DESCRIZIONE asc", par.OracleConn)
                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "tab_servizi_voci")
                    DataGridVoci.DataSource = ds
                    DataGridVoci.DataBind()
                Else
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO AS IDVOCESERVIZIO,trim(TO_CHAR(pf_voci_importo.VALORE_CONSUMO,'999999999990D99')) AS IMPORTOCONSUMO,trim(TO_CHAR(pf_voci_importo.VALORE_CANONE,'999999999990D99'))AS IMPORTO,pf_voci_importo.id, pf_voci_importo.DESCRIZIONE,pf_voci_importo.IVA_CANONE AS IVA,pf_voci_importo.IVA_CONSUMO AS IVACONSUMO,pf_voci_importo.PERC_REVERSIBILITA from siscom_mi.pf_voci_importo where   PF_VOCI_IMPORTO.ID_SERVIZIO=" & idServizio.Value & " AND id_voce=" & idVoce.Value & " and id_lotto=" & idLotto.Value & " union select TAB_SERVIZI_VOCI.ID AS IDVOCESERVIZIO,'0,00' as IMPORTOCONSUMO,'0,00' as IMPORTO,-1 as id,tab_servizi_voci.descrizione,tab_servizi_voci.iva_CANONE as iva, tab_servizi_voci.iva_consumo as ivaconsumo, tab_servizi_voci.perc_reversibilita from siscom_mi.tab_servizi_voci where  TAB_SERVIZI_VOCI.ID_VOCE=" & idVoce.Value & " AND tab_servizi_voci.id_servizio=" & idServizio.Value & " and tab_servizi_voci.perc_reversibilita" & reve & " AND (ID) NOT IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_LOTTO=" & idLotto.Value & ") ORDER BY DESCRIZIONE ASC", par.OracleConn)

                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "pf_voci_importo")
                    DataGridVoci.DataSource = ds
                    DataGridVoci.DataBind()
                End If
                myReader.Close()
            Else
                par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & idVoce.Value & " and id_lotto=" & idLotto.Value & " AND ID_SERVIZIO=" & idServizio.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = False Then
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TAB_SERVIZI_VOCI.ID AS IDVOCESERVIZIO,'0,00' as IMPORTOCONSUMO,'0,00' as IMPORTO,'-1' as id,tab_servizi_voci.descrizione,tab_servizi_voci.iva_CANONE as iva, tab_servizi_voci.iva_consumo as ivaconsumo, tab_servizi_voci.perc_reversibilita from siscom_mi.tab_servizi_voci where TAB_SERVIZI_VOCI.ID_VOCE=" & idVoce.Value & " AND tab_servizi_voci.id_servizio=" & idServizio.Value & " and tab_servizi_voci.perc_reversibilita" & reve & " order by DESCRIZIONE asc", par.OracleConn)
                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "tab_servizi_voci")
                    DataGridVoci.DataSource = ds
                    DataGridVoci.DataBind()
                Else
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO AS IDVOCESERVIZIO,trim(TO_CHAR(pf_voci_importo.VALORE_CONSUMO,'999999999990D99')) AS IMPORTOCONSUMO,trim(TO_CHAR(pf_voci_importo.VALORE_CANONE,'999999999990D99'))AS IMPORTO,pf_voci_importo.id, pf_voci_importo.DESCRIZIONE,pf_voci_importo.IVA_CANONE AS IVA,pf_voci_importo.IVA_CONSUMO AS IVACONSUMO,pf_voci_importo.PERC_REVERSIBILITA from siscom_mi.pf_voci_importo where  PF_VOCI_IMPORTO.ID_SERVIZIO=" & idServizio.Value & " AND id_voce=" & idVoce.Value & " and id_lotto=" & idLotto.Value & " union select TAB_SERVIZI_VOCI.ID AS IDVOCESERVIZIO,'0,00' as IMPORTOCONSUMO,'0,00' as IMPORTO,-1 as id,tab_servizi_voci.descrizione,tab_servizi_voci.iva_CANONE as iva, tab_servizi_voci.iva_consumo as ivaconsumo, tab_servizi_voci.perc_reversibilita from siscom_mi.tab_servizi_voci where  TAB_SERVIZI_VOCI.ID_VOCE=" & idVoce.Value & " AND tab_servizi_voci.id_servizio=" & idServizio.Value & " and tab_servizi_voci.perc_reversibilita" & reve & " AND (ID) NOT IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_LOTTO=" & idLotto.Value & ") ORDER BY DESCRIZIONE ASC", par.OracleConn)

                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "pf_voci_importo")
                    DataGridVoci.DataSource = ds
                    DataGridVoci.DataBind()
                End If
                myReader.Close()
            End If


            Dim i As Integer = 0
            Dim di As DataGridItem

            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)



                If Me.DataGridVoci.Items(i).Cells(0).Text <> "-1" Then
                    par.cmd.CommandText = "select * from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & Me.DataGridVoci.Items(i).Cells(0).Text & " and id_lotto=" & idLotto.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.HasRows = True Then
                        'DirectCast(di.Cells(16).FindControl("txtIdVoceServizio"), Image).Visible = True
                        di.Cells(16).Visible = True
                    Else
                        'DirectCast(di.Cells(16).FindControl("txtIdVoceServizio"), Image).Visible = False
                        di.Cells(16).Visible = False
                    End If
                    myReader1.Close()
                Else
                    di.Cells(16).Visible = False
                End If

            Next

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            AggiungiModifiche()
            aggiungiDivisione()



        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        CaricaIva()
    End Function

    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)
                CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(di.Cells(3).FindControl("txtImportoConsumo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Next



        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub AggiungiModifiche()
        If PROVENIENZAda.Value = "1" Then
            For Each Items As DataGridItem In DataGridVoci.Items
                DirectCast(Items.FindControl("txtImporto"), TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modifiche').value=document.getElementById('Modifiche').value+'" & Items.Cells(0).Text & "*'")
                'DirectCast(Items.FindControl("txtImporto"), TextBox).Attributes.Add("onchange", "javascript:alert('CAMBIATO!')")
            Next
        End If
    End Sub
    Protected Sub aggiungiDivisione()
        If PROVENIENZAda.Value = "1" Then
            For Each Items As DataGridItem In DataGridVoci.Items
                If Items.Cells(16).Visible = True Then
                    Divisione.Value = Divisione.Value & Items.Cells(0).Text & "*"
                End If
            Next
        End If
    End Sub

    Function CaricaGrigliaDefault()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select tab_servizi_voci.*,'0,00' as IMPORTO from siscom_mi.tab_servizi_voci where tab_servizi_voci.id_servizio=" & idServizio.Value & " order by id asc", par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "tab_servizi_voci")
            DataGridVoci.DataSource = ds
            DataGridVoci.DataBind()


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Function

    Function CaricaLordo()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select SUM((pf_voci_importo.VALORE_CONSUMO+((pf_voci_importo.VALORE_CONSUMO*pf_voci_importo.IVA_CONSUMO)/100))+(pf_voci_importo.VALORE_CANONE+((pf_voci_importo.VALORE_CANONE*pf_voci_importo.IVA_CANONE)/100))) from siscom_mi.pf_voci_importo where PF_VOCI_IMPORTO.ID_SERVIZIO=" & idServizio.Value & " AND id_voce=" & idVoce.Value & " and id_lotto=" & idLotto.Value & " ORDER BY descrizione ASC"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                TotaleLordo.Text = Format(CDbl(par.IfNull(myReader5(0), "0")), "##,##0.00")
            End If
            myReader5.Close()

            par.cmd.CommandText = "select SUM(VALORE_lordo) from siscom_mi.pf_voci_STRUTTURA where PF_VOCI_STRUTTURA.ID_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblTotaleVoce.Text = Format(CDbl(par.IfNull(myReader5(0), 0)), "##,##0.00")
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


    Function CaricaValori()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()

            par.cmd.CommandText = "select codice,descrizione from siscom_mi.pf_voci where id=" & idVoce.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = myReader5("codice") & "-" & myReader5("descrizione")

                If par.IfNull(myReader5("codice"), "") = "2.02.01" Or par.IfNull(myReader5("codice"), "") = "2.02.02" Or par.IfNull(myReader5("codice"), "") = "2.02.03" Then
                    reversibilita.Value = "100"
                Else
                    reversibilita.Value = "0"
                End If

            End If
            myReader5.Close()


            par.cmd.CommandText = "select descrizione from siscom_mi.tab_servizi where id=" & idServizio.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblServizio.Text = myReader5("descrizione")
            End If
            myReader5.Close()


            par.cmd.CommandText = "select * from siscom_mi.lotti where id=" & idLotto.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                If par.IfNull(myReader5("TIPO"), "E") = "E" Then
                    lblLotto.Text = myReader5("descrizione") & " - Lotto composto da EDIFICI"
                    TIPOIMPIANTO.Value = "0"
                Else
                    lblLotto.Text = myReader5("descrizione") & " - Lotto composto da IMPIANTI"
                    TIPOIMPIANTO.Value = "1"
                End If
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


    Protected Sub ImgProcedi0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi0.Click
        If indietro.Value = "0" Or indietro.Value = "" Then
            Response.Redirect("Lotto.aspx?IDV=" & idVoce.Value & "&IDP=" & idPianoF.Value & "&IDS=" & idServizio.Value)
        End If
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        ' IN QUESTA PROCEDURA SONO DISABILITATE LE REVERSIBILITA
        Try
            'CaricaGriglia()

            If Request.QueryString("PROV") = "PREVENTIVI" Then
                If Conferma.Value = "1" Then
                    Dim salta As Boolean = False
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans


                    Dim di As DataGridItem
                    Dim IdIntestatario As String = ""
                    Dim IdUi As String = ""
                    Dim Importo As String = ""
                    Dim ImportoConsumo As String = ""

                    Dim TotVoce As Decimal = 0
                    Dim TotaleTotale As Decimal = 0
                    Dim IDVOCESERVIZIO As Long = 0
                    Dim iva As Integer = 0
                    Dim importoIvato As Decimal = 0






                    For i = 0 To Me.DataGridVoci.Items.Count - 1
                        di = Me.DataGridVoci.Items(i)
                        Importo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text, "0,00")
                        ImportoConsumo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text, "0,00")
                        IDVOCESERVIZIO = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtIdVoceServizio"), TextBox).Text, "0")
                        iva = CInt(DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text)
                        importoIvato = Math.Round(Importo + Importo * iva / 100, 2)

                        'seleziono il lotto disponibilità della struttura in questione
                        Dim idlottodisponibilita As String = ""
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LOTTI,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                            & "WHERE LOTTI.ID_eSERCIZIO_FINANZIARIO = T_eSERCIZIO_FINANZIARIO.ID " _
                            & "AND T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                            & "AND PF_MAIN.ID='" & idPianoF.Value & "' " _
                            & "AND TIPO='X' " _
                            & "AND ID_FILIALE='" & Session.Item("ID_STRUTTURA") & "' "
                        Dim LettoreIDlotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreIDlotto.Read Then
                            idlottodisponibilita = par.IfNull(LettoreIDlotto("id"), "")
                        End If
                        LettoreIDlotto.Close()

                        par.cmd.CommandText = "SELECT PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO AS IDVOCESERVIZIO," _
                            & "TRIM (TO_CHAR (pf_voci_importo.VALORE_CONSUMO, '999999999990D99')) aS IMPORTOCONSUMO," _
                            & "TRIM (TO_CHAR (pf_voci_importo.VALORE_CANONE, '999999999990D99')) AS IMPORTO," _
                            & "pf_voci_importo.id,pf_voci_importo.DESCRIZIONE,pf_voci_importo.IVA_CANONE AS IVA," _
                            & "pf_voci_importo.IVA_CONSUMO AS IVACONSUMO,pf_voci_importo.PERC_REVERSIBILITA,PF_VOCI_IMPORTO.ID AS PFVOCIIMPORTOID " _
                            & "FROM siscom_mi.pf_voci_importo " _
                            & "WHERE PF_VOCI_IMPORTO.ID_SERVIZIO = " & idServizio.Value _
                            & " AND id_voce = " & idVoce.Value _
                            & " AND id_lotto = " & idLotto.Value _
                            & " and PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO=" & IDVOCESERVIZIO

                        Dim lettoreImporto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim impOriginale As Decimal = 0
                        Dim impOriginaleIvato As Decimal = 0
                        Dim IDPFVOCEIMPORTO As String = ""
                        If lettoreImporto.Read Then
                            IDPFVOCEIMPORTO = par.IfNull(lettoreImporto("PFVOCIIMPORTOID"), "")
                            impOriginale += par.IfNull(lettoreImporto("importo"), 0)
                            impOriginaleIvato += Math.Round(par.IfNull(lettoreImporto("importo"), 0) + par.IfNull(lettoreImporto("importo"), 0) * par.IfNull(lettoreImporto("iva"), 0) / 100, 2)
                        End If
                        lettoreImporto.Close()
                        Dim impOriginaleOLD As Decimal = impOriginale
                        Dim impOriginaleOLDIvato As Decimal = impOriginaleIvato
                        par.cmd.CommandText = "SELECT PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO AS IDVOCESERVIZIO," _
                            & "TRIM (TO_CHAR (pf_voci_importo.VALORE_CONSUMO, '999999999990D99')) aS IMPORTOCONSUMO," _
                            & "TRIM (TO_CHAR (pf_voci_importo.VALORE_CANONE, '999999999990D99')) AS IMPORTO," _
                            & "pf_voci_importo.id,pf_voci_importo.DESCRIZIONE,pf_voci_importo.IVA_CANONE AS IVA," _
                            & "pf_voci_importo.IVA_CONSUMO AS IVACONSUMO,pf_voci_importo.PERC_REVERSIBILITA " _
                            & "FROM siscom_mi.pf_voci_importo " _
                            & "WHERE PF_VOCI_IMPORTO.ID_SERVIZIO = " & idServizio.Value _
                            & " AND id_voce = " & idVoce.Value _
                            & " AND id_lotto = " & idlottodisponibilita _
                            & " and PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO=" & IDVOCESERVIZIO
                        lettoreImporto = par.cmd.ExecuteReader
                        If lettoreImporto.Read Then
                            impOriginale += par.IfNull(lettoreImporto("importo"), 0)
                            impOriginaleIvato += Math.Round(par.IfNull(lettoreImporto("importo"), 0) + par.IfNull(lettoreImporto("importo"), 0) * par.IfNull(lettoreImporto("iva"), 0) / 100, 2)
                        End If
                        lettoreImporto.Close()

                        If Not String.IsNullOrEmpty(Importo) Then
                            If Me.DataGridVoci.Items(i).Cells(0).Text = "-1" Then
                                par.cmd.CommandText = "insert into siscom_mi.pf_voci_importo (id,id_voce,id_lotto,descrizione,valore_canone,iva_canone,valore_consumo,iva_consumo,perc_reversibilita,ID_SERVIZIO,ID_VOCE_SERVIZIO) values (siscom_mi.seq_pf_voci_importo.nextval," & idVoce.Value & "," & idLotto.Value & ",'" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text) & "'," & par.VirgoleInPunti(Importo) & "," & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & "," & par.VirgoleInPunti(ImportoConsumo) & "," & DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text & "," & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & "," & idServizio.Value & "," & IDVOCESERVIZIO & ")"
                                par.cmd.ExecuteNonQuery()
                            Else
                                If CDec(importoIvato) <= impOriginaleIvato And CDec(importoIvato) >= CalcolaAppalti(idLotto.Value, IDPFVOCEIMPORTO) And (CDec(importoIvato) <> impOriginaleIvato Or CDec(importoIvato) <> impOriginaleOLDIvato) Then

                                    If DEL.Value = "0" And Replace(Modifiche.Value, "*", "") <> "" Then
                                        'se clicco su salva modificando dove è già stato diviso va cancellata la divisione per tutte
                                        'le voci che erano già state divise precedentemente
                                        Dim indice As Integer = 0
                                        For indice = 0 To Me.DataGridVoci.Items.Count - 1
                                            If InStr(Modifiche.Value, (DataGridVoci.Items.Item(indice).Cells(0).Text)) <> 0 Then
                                                Dim voceDivisa As String = DataGridVoci.Items.Item(indice).Cells(0).Text
                                                par.cmd.CommandText = "DELETE FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_VOCE_IMPORTO=" & voceDivisa & " AND ID_LOTTO=" & idLotto.Value
                                                par.cmd.ExecuteNonQuery()
                                                Modifiche.Value = Replace(Modifiche.Value, voceDivisa, "")
                                                Divisione.Value = Replace(Divisione.Value, voceDivisa, "")
                                            End If
                                        Next
                                    End If
                                    If DEL.Value = "1" Then
                                        'eliminazione della divisione precedente effettuata sulla voce
                                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI WHERE ID_VOCE_IMPORTO=" & VOCEp.Value & " AND ID_LOTTO=" & idLotto.Value
                                        par.cmd.ExecuteNonQuery()
                                        Modifiche.Value = Replace(Modifiche.Value, VOCEp.Value, "")
                                        Divisione.Value = Replace(Divisione.Value, VOCEp.Value, "")
                                    End If


                                    par.cmd.CommandText = "update siscom_mi.pf_voci_importo set valore_consumo=" & par.VirgoleInPunti(ImportoConsumo) & ",iva_consumo=" & DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text & ", descrizione='" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text) & "',valore_canone=" & par.VirgoleInPunti(Importo) & ",iva_canone=" & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & ",perc_reversibilita=" & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & " where id=" & Me.DataGridVoci.Items(i).Cells(0).Text
                                    par.cmd.ExecuteNonQuery()

                                    'controllo che sia presente la voce tesoretto
                                    par.cmd.CommandText = "select * from SISCOM_MI.pf_voci_importo where id_lotto=" & idlottodisponibilita & " and id_Voce=" & idVoce.Value & " and id_voce_servizio=" & IDVOCESERVIZIO
                                    Dim LettoreIDvoceT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettoreIDvoceT.Read Then
                                        'è presente già la voce tesoretto, quindi l'aggiorno
                                        Dim diffImportiivati As Decimal = Math.Round(CDec(impOriginaleIvato - importoIvato), 2)
                                        par.cmd.CommandText = "update siscom_mi.pf_voci_importo set valore_canone=" & par.VirgoleInPunti(CDec(diffImportiivati / (1 + iva / 100))) & ",iva_canone='" & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & "' where id=" & par.IfNull(LettoreIDvoceT("id"), "")
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        'la voce tesoretto non è presente, quindi la creo
                                        Dim diffImportiivati As Decimal = Math.Round(CDec(impOriginaleIvato - importoIvato), 2)
                                        par.cmd.CommandText = "insert into siscom_mi.pf_voci_importo (id,id_voce,id_lotto,descrizione,valore_canone,iva_canone,valore_consumo,iva_consumo,perc_reversibilita,ID_SERVIZIO,ID_VOCE_SERVIZIO) values (siscom_mi.seq_pf_voci_importo.nextval," & idVoce.Value & "," & idlottodisponibilita & ",'" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text) & "'," & par.VirgoleInPunti(CDec(diffImportiivati / (1 + iva / 100))) & "," & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & "," & par.VirgoleInPunti(ImportoConsumo) & "," & DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text & "," & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & "," & idServizio.Value & "," & IDVOCESERVIZIO & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                    LettoreIDvoceT.Close()

                                Else
                                    Modifiche.Value = Replace(Modifiche.Value, IDPFVOCEIMPORTO, "")
                                    Divisione.Value = Replace(Divisione.Value, IDPFVOCEIMPORTO, "")
                                    If CDec(Importo) > impOriginale Then
                                        Response.Write("<script>alert('L\'importo inserito per la voce " & Replace(par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text), "'", "\'") & " ha superato l\'importo massimo pari a Euro " & Format(impOriginale, "##,##0.00") & "!');</script>")
                                        salta = True

                                    ElseIf CDec(Importo) < CalcolaAppalti(idLotto.Value, IDPFVOCEIMPORTO) Then
                                        Response.Write("<script>alert('L\'importo inserito per la voce " & Replace(par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text), "'", "\'") & " è inferiore al minimo consentito pari a Euro " & Format(CalcolaAppalti(idLotto.Value, IDPFVOCEIMPORTO), "##,##0.00") & "!');</script>")
                                        salta = True
                                    End If
                                End If
                            End If
                            TotVoce = TotVoce + (Importo * (1 + (DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text / 100))) + (ImportoConsumo * (1 + (DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text / 100)))
                        Else
                            Response.Write("<script>alert('Gli importi non possono essere vuoti. Se non si desidera inserire nessun valore, inserire 0,00');</script>")
                            Exit Sub
                        End If
                    Next

                    par.cmd.CommandText = "select sum((valore_canone*(1+(iva_canone/100)))+(valore_consumo*(1+(iva_consumo/100)))) from siscom_mi.pf_voci_importo where id_voce=" & idVoce.Value & " AND ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_FILIALE=" & Session.Item("ID_STRUTTURA") & ")"
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.Read Then
                        par.cmd.CommandText = "update siscom_mi.pf_voci_STRUTTURA set valore_netto=0,iva=0, valore_lordo=" & par.VirgoleInPunti(par.IfNull(myReader5(0), 0)) & " where id_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                        par.cmd.ExecuteNonQuery()
                        TotaleTotale = CDbl(par.IfNull(myReader5(0), 0))
                    End If
                    myReader5.Close()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                        & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F84',''," & Session.Item("ID_STRUTTURA") & ")"
                    par.cmd.ExecuteNonQuery()
                    par.myTrans.Commit()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    CaricaLordo()
                    txtmodificato.Value = "0"

                    If DEL.Value = "1" Then
                        Dividip.Value = "1"
                    Else
                        If salta = False Then
                            Response.Write("<script>alert('Operazione Effettuata!');</script>")
                        End If
                    End If


                    If TotaleLordoRichiesto.Visible = True Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)

                        If TotaleTotale = CDbl(TotaleLordoRichiesto.Text) Then
                            Response.Write("<script>alert('ATTENZIONE...gli importi inseriti per la voce scelta, corrispondono a quanto approvato dal Gestore.\nLa voce sarà definita automaticamente completa!');</script>")
                            par.cmd.CommandText = "update siscom_mi.pf_voci_STRUTTURA set completo='1' where id_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                            par.cmd.ExecuteNonQuery()
                        End If
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        ' Response.Write("<script>opener.form1.submit();</script>")
                    End If
                End If

            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim IdIntestatario As String = ""
                Dim IdUi As String = ""
                Dim Importo As String = ""
                Dim ImportoConsumo As String = ""

                Dim TotVoce As Double = 0
                Dim TotaleTotale As Double = 0
                Dim IDVOCESERVIZIO As Long = 0

                For i = 0 To Me.DataGridVoci.Items.Count - 1
                    di = Me.DataGridVoci.Items(i)
                    Importo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text, "0,00")
                    ImportoConsumo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoConsumo"), TextBox).Text, "0,00")
                    IDVOCESERVIZIO = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtIdVoceServizio"), TextBox).Text, "0")

                    If Not String.IsNullOrEmpty(Importo) Then
                        If Me.DataGridVoci.Items(i).Cells(0).Text = "-1" Then
                            par.cmd.CommandText = "insert into siscom_mi.pf_voci_importo (id,id_voce,id_lotto,descrizione,valore_canone,iva_canone,valore_consumo,iva_consumo,perc_reversibilita,ID_SERVIZIO,ID_VOCE_SERVIZIO) values (siscom_mi.seq_pf_voci_importo.nextval," & idVoce.Value & "," & idLotto.Value & ",'" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text) & "'," & par.VirgoleInPunti(Importo) & "," & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & "," & par.VirgoleInPunti(ImportoConsumo) & "," & DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text & "," & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & "," & idServizio.Value & "," & IDVOCESERVIZIO & ")"
                        Else
                            par.cmd.CommandText = "update siscom_mi.pf_voci_importo set valore_consumo=" & par.VirgoleInPunti(ImportoConsumo) & ",iva_consumo=" & DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text & ", descrizione='" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(12).Text) & "',valore_canone=" & par.VirgoleInPunti(Importo) & ",iva_canone=" & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & ",perc_reversibilita=" & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & " where id=" & Me.DataGridVoci.Items(i).Cells(0).Text
                        End If
                        par.cmd.ExecuteNonQuery()

                        TotVoce = TotVoce + (Importo * (1 + (DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text / 100))) + (ImportoConsumo * (1 + (DirectCast(di.Cells(1).FindControl("cmbIvaConsumo"), DropDownList).Text / 100)))

                    Else
                        Response.Write("<script>alert('Gli importi non possono essere vuoti. Se non si desidera inserire nessun valore, inserire 0,00');</script>")
                        Exit Sub
                    End If

                Next

                par.cmd.CommandText = "select sum((valore_canone*(1+(iva_canone/100)))+(valore_consumo*(1+(iva_consumo/100)))) from siscom_mi.pf_voci_importo where id_voce=" & idVoce.Value & " AND ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_FILIALE=" & Session.Item("ID_STRUTTURA") & ")"
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.Read Then
                    par.cmd.CommandText = "update siscom_mi.pf_voci_STRUTTURA set valore_netto=0,iva=0, valore_lordo=" & par.VirgoleInPunti(par.IfNull(myReader5(0), 0)) & " where id_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                    par.cmd.ExecuteNonQuery()
                    TotaleTotale = CDbl(par.IfNull(myReader5(0), 0))
                End If
                myReader5.Close()




                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F84',''," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()
                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaLordo()
                txtmodificato.Value = "0"
                Response.Write("<script>alert('Operazione Effettuata!');</script>")

                If TotaleLordoRichiesto.Visible = True Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    If TotaleTotale = CDbl(TotaleLordoRichiesto.Text) Then
                        Response.Write("<script>alert('ATTENZIONE...gli importi inseriti per la voce scelta, corrispondono a quanto approvato dal Gestore.\nLa voce sarà definita automaticamente completa!');</script>")
                        par.cmd.CommandText = "update siscom_mi.pf_voci_STRUTTURA set completo='1' where id_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                        par.cmd.ExecuteNonQuery()
                    End If
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    ' Response.Write("<script>opener.form1.submit();</script>")
                End If



            End If




            CaricaGriglia()
            AddJavascriptFunction()

            AggiornaOP.Value = "1"

        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try



        'IN QUESTA PROCEDURA SONO ABILITATE LE REVERSIBILITA
        'Try
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)
        '    par.myTrans = par.OracleConn.BeginTransaction()
        '    ‘‘par.cmd.Transaction = par.myTrans

        '    Dim i As Integer = 0
        '    Dim di As DataGridItem
        '    Dim IdIntestatario As String = ""
        '    Dim IdUi As String = ""
        '    Dim Importo As String = ""

        '    Dim TotVoce As Double = 0

        '    For i = 0 To Me.DataGridVoci.Items.Count - 1
        '        di = Me.DataGridVoci.Items(i)
        '        Importo = DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text

        '        If Not String.IsNullOrEmpty(Importo) Then
        '            If Me.DataGridVoci.Items(i).Cells(0).Text = "-1" Then
        '                par.cmd.CommandText = "insert into siscom_mi.pf_voci_importo (id,id_voce,id_lotto,descrizione,valore,iva,perc_reversibilita,freq_pagamento,ID_SERVIZIO) values (siscom_mi.seq_pf_voci_importo.nextval," & idVoce.Value & "," & idLotto.Value & ",'" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(8).Text) & "'," & par.VirgoleInPunti(Importo) & "," & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & "," & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & "," & DirectCast(di.Cells(1).FindControl("cmbfreqpagamento"), DropDownList).SelectedItem.Value & "," & idServizio.Value & ")"
        '            Else
        '                par.cmd.CommandText = "update siscom_mi.pf_voci_importo set freq_pagamento=" & DirectCast(di.Cells(1).FindControl("cmbfreqpagamento"), DropDownList).SelectedItem.Value & ",descrizione='" & par.PulisciStrSql(Me.DataGridVoci.Items(i).Cells(8).Text) & "',valore=" & par.VirgoleInPunti(Importo) & ",iva=" & DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text & ",perc_reversibilita=" & DirectCast(di.Cells(1).FindControl("cmbRev"), DropDownList).Text & " where id=" & Me.DataGridVoci.Items(i).Cells(0).Text
        '            End If
        '            par.cmd.ExecuteNonQuery()

        '            TotVoce = TotVoce + (Importo * (1 + (DirectCast(di.Cells(1).FindControl("cmbIva"), DropDownList).Text / 100)))

        '        Else
        '            Response.Write("<script>alert('Gli importi non possono essere vuoti. Se non si desidera inserire nessun valore, inserire 0,00');</script>")
        '            Exit Sub
        '        End If

        '    Next

        '    par.cmd.CommandText = "select sum(valore*(1+(iva/100))) from siscom_mi.pf_voci_importo where id_voce=" & idVoce.Value
        '    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReader5.Read Then
        '        par.cmd.CommandText = "update siscom_mi.pf_voci set valore=" & par.VirgoleInPunti(par.IfNull(myReader5(0), 0)) & " where id=" & idVoce.Value
        '        par.cmd.ExecuteNonQuery()
        '    End If
        '    myReader5.Close()




        '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '                        & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '                        & "'F84','')"
        '    par.cmd.ExecuteNonQuery()



        '    par.myTrans.Commit()
        '    par.cmd.Dispose()
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '    txtmodificato.Value = "0"
        '    Response.Write("<script>alert('Operazione Effettuata!');</script>")
        '    CaricaGriglia()
        '    AddJavascriptFunction()

        'Catch ex As Exception
        '    par.myTrans.Rollback()
        '    par.cmd.Dispose()
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        'End Try
    End Sub

    Protected Sub imgRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRefresh.Click
        CaricaGriglia()
    End Sub


    Function CalcolaAppalti(ByVal idLotto As String, ByVal idPfVoce As String) As Decimal
        Dim totale As Decimal = 0
        Try
            'par.cmd.CommandText = "SELECT IMPORTO_CANONE,SCONTO_CANONE, APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CANONE, IVA_CANONE, " _
            '                    & "IMPORTO_CONSUMO,SCONTO_CONSUMO,APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CONSUMO,IVA_CONSUMO " _
            '                     & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_SERVIZI.ID_APPALTO " _
            '                     & " AND ID_LOTTO = " & idLotto _
            '                     & " AND ID_PF_VOCE_IMPORTO = " & idPfVoce
            'Dim appoggio As Decimal = 0

            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)

            'For Each riga As Data.DataRow In dt.Rows
            '    appoggio = CDec(par.IfNull(riga.Item("IMPORTO_CANONE"), 0)) - Math.Round(((CDec(par.IfNull(riga.Item("IMPORTO_CANONE"), 0)) * par.IfNull(riga.Item("SCONTO_CANONE"), 0)) / 100), 4)
            '    appoggio = appoggio + par.IfNull(riga.Item("ONERI_SICUREZZA_CANONE"), 0)
            '    appoggio = appoggio + ((appoggio * par.IfNull(riga.Item("IVA_CANONE"), 0)) / 100)
            '    totale = totale + Math.Round(appoggio, 2)
            '    appoggio = 0
            '    appoggio = CDec(par.IfNull(riga.Item("IMPORTO_CONSUMO"), 0)) - Math.Round(((CDec(par.IfNull(riga.Item("IMPORTO_CONSUMO"), 0)) * par.IfNull(riga.Item("SCONTO_CONSUMO"), 0)) / 100), 4)
            '    appoggio = appoggio + par.IfNull(riga.Item("ONERI_SICUREZZA_CONSUMO"), 0)
            '    appoggio = appoggio + ((appoggio * par.IfNull(riga.Item("IVA_CONSUMO"), 0)) / 100)
            '    totale = totale + Math.Round(appoggio, 2)
            '    appoggio = 0

            'Next

            'modifica del 29/03/2012
            'l'importo è modificabile fino alla cifra di copertura delle prenotazioni
            par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO_PRENOTATO,0)) FROM SISCOM_MI.PRENOTAZIONI,APPALTI WHERE ID_VOCE_PF_IMPORTO=" & idPfVoce & " AND APPALTI.ID=PRENOTAZIONI.ID_APPALTO AND ID_LOTTO=" & idLotto
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                totale = par.IfNull(LETTORE(0), 0)
            End If
            LETTORE.Close()
        Catch ex As Exception

        End Try

        Return totale
    End Function

    Private Sub CaricaIva()
        Try
            For Each RIGA As DataGridItem In DataGridVoci.Items
                par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", CType(RIGA.FindControl("cmbIva"), DropDownList), "VALORE", "VALORE", False)
                par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", CType(RIGA.FindControl("cmbIvaConsumo"), DropDownList), "VALORE", "VALORE", False)
                CType(RIGA.FindControl("cmbIva"), DropDownList).SelectedValue = par.IfNull(RIGA.Cells(3).Text, 0)
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
