
Partial Class Contratti_BolletteDaPagare
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        Response.Write(Loading)
        Response.Flush()

        If Not IsPostBack Then
            idContratto.Value = Request.QueryString("IDCONTR")
            idGest.Value = Request.QueryString("IDGEST")
            RiempiTabella()
        End If
    End Sub

    Private Sub RiempiTabella()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim importoTotale As Decimal = 0
            Dim importoPagato As Decimal = 0
            Dim nMesi As Integer = 0
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idGest.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                lblCredito.Text = "Credito €." & Format(Math.Abs(par.IfNull(myReader("IMPORTO_TOTALE"), 0)), "##,##0.00")
                impCredito = Math.Abs(par.IfNull(myReader("IMPORTO_TOTALE"), 0))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(ID) as statoContr FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContratto.Value & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                statoContratto.Value = par.IfNull(myReader0("statoContr"), "")
            End If
            myReader0.Close()

            Dim condizioneSql As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=43"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                nMesi = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()
            If Session.Item("FL_FORZA_SCADENZA") = 0 Then
                condizioneSql = " and ADD_MONTHS(TO_DATE (bol_bollette.data_scadenza, 'yyyyMMdd'), " & nMesi & ") <= TO_DATE ('" & Format(Now, "yyyyMMdd") & "', 'yyyyMMdd') "
            End If
            par.cmd.CommandText = "SELECT bol_bollette.ID, bol_bollette.note, " _
                        & "TO_CHAR(TO_DATE(bol_bollette.riferimento_da,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_da, " _
                        & "TO_CHAR(TO_DATE(bol_bollette.riferimento_a,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_a, " _
                        & "TO_CHAR(TO_DATE(bol_bollette.data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS data_emissione, " _
                        & "trim(TO_CHAR((NVL(bol_bollette.importo_totale,0) - NVL(bol_bollette.QUOTA_SIND_B,0)),'9G999G999G999G990D99')) AS importo_totale,  " _
                        & "trim(TO_CHAR((NVL(bol_bollette.importo_pagato,0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0)),'9G999G999G999G990D99')) AS importo_pagato , " _
                        & "TO_CHAR(TO_DATE(bol_bollette.data_scadenza,'yyyymmdd'),'dd/mm/yyyy')AS data_scadenza,  " _
                        & "TIPO_BOLLETTE.ACRONIMO from siscom_mi.bol_bollette,SISCOM_MI.TIPO_BOLLETTE where id_contratto=" & idContratto.Value & " " _
                        & condizioneSql _
                        & " and (NVL (abs(bol_bollette.importo_totale), 0) - NVL (abs(bol_bollette.QUOTA_SIND_B), 0))>NVL (abs(bol_bollette.importo_pagato), 0)- NVL (abs(bol_bollette.QUOTA_SIND_PAGATA_B), 0) and abs(importo_totale)>0 and fl_annullata=0 and id_tipo<> 22 and " _
                        & " /*bol_bollette.id NOT IN (SELECT id_bolletta FROM siscom_mi.bol_bollette_voci bbv,siscom_mi.bol_bollette bb WHERE bbv.id_bolletta = bb.id and bb.id_contratto=bol_bollette.id_contratto AND bbv.id_voce IN (182)) and */ BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND NVL (id_rateizzazione, 0) = 0 and NVL (importo_ruolo, 0) = 0 AND NVL (id_bolletta_ric, 0) = 0 and NVL (importo_ingiunzione, 0) = 0 order by bol_bollette.data_scadenza asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da.Fill(dt0)
            For Each row As Data.DataRow In dt0.Rows
                importoTotale = importoTotale + row.Item("IMPORTO_TOTALE")
                importoPagato = importoPagato + row.Item("IMPORTO_PAGATO")
            Next

            DgvBolDaPagare.DataSource = dt0
            DgvBolDaPagare.DataBind()
            lblNumBoll.Text = "Totale bollette: " & dt0.Rows.Count & " - Importo totale: €." & Format(Math.Round(importoTotale - importoPagato, 2), "##,##0.00")

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore nel caricamento delle bollette!');self.close();</script>")
        End Try

    End Sub

    Protected Sub chkAll_click(sender As Object, e As System.EventArgs)
        Try
            Dim impSelezionato As Decimal = 0
            If Selezionati.Value = 0 Then

                For Each di As DataGridItem In DgvBolDaPagare.Items
                    DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = True
                    impSelezionato += CDec(par.IfEmpty(di.Cells(5).Text, 0)) - CDec(par.IfEmpty(di.Cells(6).Text, 0))
                Next
                Selezionati.Value = 1
            Else
                For Each di As DataGridItem In DgvBolDaPagare.Items
                    DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = False
                Next
                Selezionati.Value = 0
            End If
            If impCredito - impSelezionato > 0 Then
                If statoContratto.Value <> "CHIUSO" Then
                    If Session.Item("FL_SCELTA_DEST_ECCED") = "1" Then
                        rdbSceltaCredito.Visible = True
                        lblScelta.Visible = True
                    End If
                End If
                lblTotSelez.Text = "Tot. selezionato da pagare: €." & Format(impSelezionato, "##,##0.00") & " - credito residuo : €." & Format((impCredito - impSelezionato), "##,##0.00") & " "
            Else
                lblTotSelez.Text = "Tot. selezionato da pagare: €." & Format(impSelezionato, "##,##0.00") & " - credito residuo: €.0"
            End If

            Dim script As String = ""
            script = "if(document.getElementById('PanelBollette')!=null){document.getElementById('PanelBollette').scrollTop = document.getElementById('yPosBoll').value;}"

            ScriptManager.RegisterStartupScript(Page, GetType(Panel), Page.ClientID, script, True)

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub chkSelected_click(sender As Object, e As System.EventArgs)
        Try
            Dim impSelezionato As Decimal = 0
            For Each di As DataGridItem In DgvBolDaPagare.Items
                If DirectCast(di.Cells(0).FindControl("ChkSelected"), CheckBox).Checked = True Then
                    impSelezionato += CDec(par.IfEmpty(di.Cells(5).Text, 0)) - CDec(par.IfEmpty(di.Cells(6).Text, 0))
                End If
            Next
            If impCredito - impSelezionato > 0 Then
                If statoContratto.Value <> "CHIUSO" Then
                    If Session.Item("FL_SCELTA_DEST_ECCED") = "1" Then
                        rdbSceltaCredito.Visible = True
                        lblScelta.Visible = True
                    End If
                End If
                lblTotSelez.Text = "Tot. selezionato da pagare: €." & Format(impSelezionato, "##,##0.00") & " - credito residuo : €." & Format((impCredito - impSelezionato), "##,##0.00") & " "
            Else
                lblTotSelez.Text = "Tot. selezionato da pagare: €." & Format(impSelezionato, "##,##0.00") & " - credito residuo: €.0"
            End If
            Dim script As String = ""
            script = "if(document.getElementById('PanelBollette')!=null){document.getElementById('PanelBollette').scrollTop = document.getElementById('yPosBoll').value;}"

            ScriptManager.RegisterStartupScript(Page, GetType(Panel), Page.ClientID, script, True)

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property dtdati() As Data.DataTable
        Get
            If Not (ViewState("dtdati") Is Nothing) Then
                Return ViewState("dtdati")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtdati") = value
        End Set
    End Property

    Public Property impCredito() As Decimal
        Get
            If Not (ViewState("par_impCredito") Is Nothing) Then
                Return CDec(ViewState("par_impCredito"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_impCredito") = value
        End Set

    End Property

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click

        Dim listaSelezionati As String = ""
        For Each riga As DataGridItem In DgvBolDaPagare.Items
            If CType(riga.FindControl("ChkSelected"), CheckBox).Checked = True Then
                If listaSelezionati = "" Then
                    listaSelezionati &= riga.Cells(par.IndDGC(DgvBolDaPagare, "ID")).Text
                Else
                    listaSelezionati &= "," & riga.Cells(par.IndDGC(DgvBolDaPagare, "ID")).Text
                End If
            End If
        Next

        Dim sceltaScritt As Integer = 0
        If Not String.IsNullOrEmpty(rdbSceltaCredito.SelectedValue) Then
            sceltaScritt = rdbSceltaCredito.SelectedValue
        End If
        'sceltaScritt = 0
        If confermaRipart.Value = "1" Then
            Response.Write("<script language='javascript'>window.open('SpostamGestionaleTot.aspx?GEST=" & sceltaScritt & "&IDBOLL=" & idGest.Value & "&IDCONTR=" & idContratto.Value & "&LBOLL=" & listaSelezionati & "&TIPO=CRED', 'SpostamCre', 'height=0,top=150,left=250,width=0');</script>")
        End If

    End Sub

End Class
