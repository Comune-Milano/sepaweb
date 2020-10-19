
Partial Class ASS_DispAler
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            'par.OracleConn.Open()
            'par.SettaCommand(par)

            par.RiempiDList(Me, par.OracleConn, "cmbPiano", "select * from siscom_mi.tipo_livello_piano order by descrizione asc", "DESCRIZIONE", "COD")
            par.RiempiDList(Me, par.OracleConn, "cmbTipo", "select * from T_TIPO_ALL_ERP ORDER BY COD ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Me, par.OracleConn, "cmbTipoVia", "select * from T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")



        End If
        imgElenco.Attributes.Add("onclick", "javascript:alert('Non disponibile!');")
        txtDisponibile.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        If txtCodice.Text = "" Then
            Response.Write("<script>alert('Inserire il codice alloggio!');</script>")
            Exit Sub
        End If
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "Insert into ALLOGGI (ID, PROPRIETA, ZONA, INDIRIZZO, NUM_CIVICO, " _
                                & "NUM_ALLOGGIO, NUM_LOCALI, NUM_SERVIZI, TIPO_ALLOGGIO, PIANO, " _
                                & "NOTE, SUP, EQCANONE, EXRESO, STATO, " _
                                & "PRENOTATO, ASSEGNATO, ID_PRATICA, ASCENSORE, GAS, " _
                                & "RISCALDAMENTO, BARRIERE_ARC, DATA_PRENOTATO, DATA_RESO, DATA_PROT, " _
                                & "SETTORE, NUM_PG, DATA_COMUNICAZIONE, DATA_DISPONIBILITA, DATA_RITRASMISSIONE, " _
                                & "PROTOCOLLI, ID_PRATICA_PRENOTATO, CUCINA, CUCININO, COD_ALLOGGIO, " _
                                & "MOTIVAZIONE_RESO, TIPO_INDIRIZZO, ZONA_ALER, SCALA, COMUNE, " _
                                & "CONDIZIONE, GESTIONE, TIPOLOGIA_GESTORE, VECCHIO_CODICE, FOGLIO, " _
                                & "PARTICELLA, SUB, H_MOTORIO, FL_POR) " _
                                & "Values " _
                                & "(seq_alloggi.nextval, 1, '" & cmbZona.SelectedItem.Value _
                                & "', '" & par.PulisciStrSql(txtIndirizzo.Text) _
                                & "', '" & par.PulisciStrSql(txtCivico.Text) & "', " _
                                & "'" & par.PulisciStrSql(txtInterno.Text) & "', '" _
                                & par.PulisciStrSql(txtLocali.Text) & "', NULL, " _
                                & cmbTipo.SelectedItem.Value & ", '" & cmbPiano.SelectedItem.Value & "', " _
                                & "'INSERITO DA WEB', " & par.VirgoleInPunti(par.IfEmpty(txtSuperficie.Text, "0")) & " , '0', '0', 5, " _
                                & "'0', '0', NULL, '" & Valore01(chAscensore.Checked) & "', '0', " _
                                & "'0', '" & Valore01(chBar.Checked) & "', '','','',NULL,NULL,'" & Format(Now, "yyyyMMdd") _
                                & "','" & par.AggiustaData(txtDisponibile.Text) & "', NULL, " _
                                & "NULL, NULL, " _
                                & "'0', '0', '" & par.PulisciStrSql(UCase(txtCodice.Text)) & "', " _
                                & "NULL, " & cmbTipoVia.SelectedItem.Value & " , 0, '" & par.PulisciStrSql(txtscala.Text) & "', '" & par.PulisciStrSql(UCase(TXTCOMUNE.Text)) & "', " _
                                & "'', 9, 'ERP', '', '" & par.PulisciStrSql(txtfoglio.Text) & "', " _
                                & "'" & par.PulisciStrSql(txtmappale.Text) & "', '" & par.PulisciStrSql(txtsub.Text) & "' , '" & Valore01(chHandicap.Checked) & "', '0')"

            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Operazione Effettuata!');</script>")
            Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")



            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Label4.Visible = True
            Label4.Text = ex.Message

        End Try
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function


End Class
