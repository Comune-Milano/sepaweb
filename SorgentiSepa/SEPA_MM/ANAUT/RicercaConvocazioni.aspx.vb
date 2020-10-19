
Partial Class ANAUT_RicercaConvocazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
 If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi ORDER BY id desc", "DESCRIZIONE", "ID")
                CaricaListe()

                If Session.Item("MOD_AU_CONV_VIS_TUTTO") = "1" Then
                    par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali WHERE ID IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID_BANDO=" & cmbBando.SelectedItem.Value & ")  order by NOME asc", "NOME", "ID")
                    cmbFiliale.Items.Add("TUTTE LE SEDI")
                    cmbFiliale.Items.FindByText("TUTTE LE SEDI").Selected = True
                    cmbOperatore.Enabled = False
                Else
                    par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali where ID IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID_BANDO=" & cmbBando.SelectedItem.Value & ")  AND id in (select id_UFFICIO from operatori where id=" & Session.Item("ID_OPERATORE") & ") order by NOME asc", "NOME", "ID")
                    cmbFiliale.Enabled = False

                    par.RiempiDList(Me, par.OracleConn, "cmbOperatore", "SELECT DISTINCT N_OPERATORE FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_FILIALE=" & cmbFiliale.SelectedItem.Value & " AND N_OPERATORE IS NOT NULL AND ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & cmbBando.SelectedItem.Value & ") ORDER BY TO_NUMBER(N_OPERATORE) ASC", "N_OPERATORE", "N_OPERATORE")
                    cmbOperatore.Enabled = True
                    cmbOperatore.Items.Add("TUTTI GLI SPORTELLI")
                    cmbOperatore.Items.FindByText("TUTTI GLI SPORTELLI").Selected = True

                End If



                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:RicercaConvocazioni - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub cmbFiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        Try
            cmbOperatore.Items.Clear()
            If cmbFiliale.SelectedItem.Text <> "TUTTE LE SEDI" Then
                Try
                    par.RiempiDList(Me, par.OracleConn, "cmbOperatore", "SELECT ID,DESCRIZIONE FROM UTENZA_SPORTELLI WHERE ID IN (SELECT DISTINCT ID_SPORTELLO FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_FILIALE=" & cmbFiliale.SelectedItem.Value & " AND DATA_APP IS NOT NULL AND ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & cmbBando.SelectedItem.Value & ")) ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")
                    cmbOperatore.Enabled = True

                    cmbOperatore.Items.Add("TUTTI GLI SPORTELLI")
                    cmbOperatore.Items.FindByText("TUTTI GLI SPORTELLI").Selected = True

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Add("ERRORE", "Provenienza:RicercaConvocazioni - " & ex.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End Try
            Else
                cmbOperatore.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If Not IsNothing(cmbLista.SelectedItem) Then
            If cmbOperatore.Enabled = True Then
                Response.Write("<script>window.open('ElencoConvocazioni.aspx?idL=" & par.CriptaMolto(cmbLista.SelectedItem.Value) & "&idB=" & par.CriptaMolto(cmbBando.SelectedItem.Value) & "&idF=" & par.CriptaMolto(cmbFiliale.SelectedItem.Value) & "&idO=" & par.CriptaMolto(cmbOperatore.SelectedItem.Value) & "','" & Format(Now, "yyyyMMddHHmmss") & "','');</script>")
            Else
                Response.Write("<script>window.open('ElencoConvocazioni.aspx?idL=" & par.CriptaMolto(cmbLista.SelectedItem.Value) & "&idB=" & par.CriptaMolto(cmbBando.SelectedItem.Value) & "&idF=" & par.CriptaMolto(cmbFiliale.SelectedItem.Value) & "&idO=" & par.CriptaMolto("-1") & "','" & Format(Now, "yyyyMMddHHmmss") & "','');</script>")
            End If
        Else
            Response.Write("<script>alert('Selezionare una lista di convocazione!');</script>")
        End If
    End Sub

    Protected Sub cmbBando_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        cARICAlISTE()
    End Sub

    Private Function CaricaListe()
        Try
            cmbLista.Items.Clear()

            Try
                par.RiempiDList(Me, par.OracleConn, "cmbLista", "SELECT * FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE FL_STAMPATA=1 AND ID_AU=" & cmbBando.SelectedItem.Value & " ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")
                cmbLista.Enabled = True

                cmbLista.Items.Add("TUTTE LE LISTE")
                cmbLista.Items.FindByText("TUTTE LE LISTE").Selected = True

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:RicercaConvocazioni - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try


        Catch ex As Exception

        End Try
    End Function

    Protected Sub cmbOperatore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOperatore.SelectedIndexChanged

    End Sub

    Protected Sub cmbLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLista.SelectedIndexChanged

    End Sub
End Class
