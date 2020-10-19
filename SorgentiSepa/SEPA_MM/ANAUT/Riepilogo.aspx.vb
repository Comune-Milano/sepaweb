
Partial Class ANAUT_Riepilogo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "select * from utenza_bandi order by id desc", "DESCRIZIONE", "ID")
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'cmbBando.Items.FindByValue("2").Selected = True
                Carica()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblBando.Text = ex.Message
            End Try
        End If
    End Sub


    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim Indice As String = cmbBando.SelectedItem.Value


            'CARICATE
            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and id_bando=" & Indice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblCaricate.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                LBLCARICATEOP.Text = myReader(0)
            End If
            myReader.Close()


            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and fl_generaz_auto=1 AND id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblAutomatiche.Text = myReader(0)
            End If
            myReader.Close()


            'COMPLETE
            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=1 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblComplete.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND id_stato=1 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblComplete0.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto=1) AND id_stato=1 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblComplete1.Text = myReader(0)
            End If
            myReader.Close()

            'INCOMPLETE

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblIncomplete.Text = myReader(0)
            End If
            myReader.Close()


            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and  (fl_generaz_auto is null or fl_generaz_auto=0) AND id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblIncomplete0.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and  (fl_generaz_auto=1) AND id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblIncomplete1.Text = myReader(0)
            End If
            myReader.Close()

            'DA CANCELLARE

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=2 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblcancellare.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND id_stato=2 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblcancellare0.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto=1) AND id_stato=2 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblcancellare1.Text = myReader(0)
            End If
            myReader.Close()



            'DA VERIFICARE


            'CARICATE
            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label1.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where  pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND  UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label2.Text = myReader(0)
            End If
            myReader.Close()


            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and fl_generaz_auto=1 AND UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' AND id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label3.Text = myReader(0)
            End If
            myReader.Close()


            'COMPLETE
            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=1 AND UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label4.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND  UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_stato=1 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label7.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto=1) AND  UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_stato=1 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label8.Text = myReader(0)
            End If
            myReader.Close()

            'INCOMPLETE

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=0 and UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label9.Text = myReader(0)
            End If
            myReader.Close()


            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and  (fl_generaz_auto is null or fl_generaz_auto=0)  AND UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label10.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and  (fl_generaz_auto=1)  AND UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label11.Text = myReader(0)
            End If
            myReader.Close()

            'DA CANCELLARE

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=2 and  UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label12.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND id_stato=2 and  UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label13.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto=1) AND  UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' and id_stato=2 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label14.Text = myReader(0)
            End If
            myReader.Close()





            'SOSP. INDAGINE


            'CARICATE
            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label15.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where  pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND  UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label16.Text = myReader(0)
            End If
            myReader.Close()


            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and fl_generaz_auto=1 AND UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' AND id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label17.Text = myReader(0)
            End If
            myReader.Close()


            'COMPLETE
            'par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=1 AND UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_bando=" & Indice
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    Label18.Text = myReader(0)
            'End If
            'myReader.Close()

            'par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND  UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_stato=1 and id_bando=" & Indice
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    Label19.Text = myReader(0)
            'End If
            'myReader.Close()

            'par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is NOT null or fl_generaz_auto=1) AND  UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_stato=1 and id_bando=" & Indice
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    Label20.Text = myReader(0)
            'End If
            'myReader.Close()

            'INCOMPLETE

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=0 and UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label21.Text = myReader(0)
            End If
            myReader.Close()


            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and  (fl_generaz_auto is null or fl_generaz_auto=0)  AND UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label22.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and  (fl_generaz_auto=1)  AND UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_stato=0 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label23.Text = myReader(0)
            End If
            myReader.Close()

            'DA CANCELLARE

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null AND id_stato=2 and  UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label24.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto is null or fl_generaz_auto=0) AND id_stato=2 and  UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label25.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "select count(id) from utenza_dichiarazioni where pg is not null and (fl_generaz_auto=1) AND  UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' and id_stato=2 and id_bando=" & Indice
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label26.Text = myReader(0)
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblBando.Text = ex.Message
        End Try
    End Function

    Protected Sub cmbBando_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        Carica()
    End Sub
End Class
