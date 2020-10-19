
Partial Class FSA_RicercaDomande
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Inserire qui il codice utente necessario per inizializzare la pagina
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            Label3.Text = "La ricerca sarà effettuata solo sulle domande acquisite dall'ente " & Session.Item("CAAF")
            'par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_STATI_DICHIARAZIONE ORDER BY COD ASC", "DESCRIZIONE", "COD")
            If cmbStato.Items.Count = 0 Then
                cmbStato.Items.Add("TUTTI")
                cmbStato.Items.Add("FORMALIZZAZIONE")
                cmbStato.Items.Add("ISTRUTTORIA")
                cmbStato.Items.Add("IDONEE")
                cmbStato.Items.Add("RESPINTE")
                cmbStato.Items.FindByText("TUTTI").Selected = True
            End If
            If cmbBando.Items.Count = 0 Then

                Dim lsiFrutto As New ListItem("TUTTI", "-2")
                cmbBando.Items.Add(lsiFrutto)

                Try

                    Dim id_bando As Long = 2011
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "SELECT * FROM BANDI_fsa WHERE ID<>-1 ORDER BY ID ASC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader.Read
                        lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                        cmbBando.Items.Add(lsiFrutto)
                        id_bando = myReader("ID")
                    End While
                    myReader.Close()
                    par.RiempiDList(Me, par.OracleConn, "cmbOperatore", "SELECT OPERATORE AS ""DESCRIZIONE"",ID FROM OPERATORI WHERE ID_CAF=" & Session.Item("ID_CAF") & " ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")
                    Dim lsiFrutto1 As New ListItem("---", "-1")
                    cmbOperatore.Items.Add(lsiFrutto1)

                    cmbOperatore.Items.FindByText("---").Selected = True


                    cmbTipo.Items.Add(New ListItem("a) RESIDENZA NELL’ALLOGGIO OGGETTO DEL CONTRATTO", 1))
                    cmbTipo.Items.Add(New ListItem("b) NON E' ASSEGNATARIO ALLOGGIO ERP O POR", 2))
                    cmbTipo.Items.Add(New ListItem("c) CITTADINANZA O SOGGIORNO", 3))
                    cmbTipo.Items.Add(New ListItem("d) ASSENZA DI PROCEDURA ESECUTIVA DI SFRATTO", 4))
                    cmbTipo.Items.Add(New ListItem("e) ASSENZA DI PROPRIETA' DI ALLOGGIO ADEGUATO", 5))

                    cmbTipo.Items.Add(New ListItem("g) ASSENZA ASSEGNAZIONE IN GODIMENTO DI U.I. DA PARTE DI COOP EDILIZIE", 7))
                    cmbTipo.Items.Add(New ListItem("h) ASSENZA DI ASSEGNAZIONE DI U.I REALIZZATE CON CONTR PUBBLICI", 8))
                    cmbTipo.Items.Add(New ListItem("i) MORTE", 9))
                    cmbTipo.Items.Add(New ListItem("l) IRREPERIBILITA’", 10))
                    cmbTipo.Items.Add(New ListItem("m) MANCATA PRESENTAZIONE DOPO DIFFIDA", 11))

                    cmbTipo.Items.Add(New ListItem("n) RINUNCIA", 12))
                    cmbTipo.Items.Add(New ListItem("o) PERMESSO DI SOGGIORNO INFERIORE AL LIMITE", 13))
                    cmbTipo.Items.Add(New ListItem("p) ASSENZA DI DICHIARAZIONE NON VERITIERA O DISCORDANTE", 14))

                    cmbTipo.Items.Add(New ListItem("CONTRIBUTO INFERIORE A 100 EURO", 15))
                    cmbTipo.Items.Add(New ListItem("CONTRIBUTO GIA RICHIESTO PER QUESTA UNITA ABITATIVA", 16))
                    cmbTipo.Items.Add(New ListItem("UNITA' IMMOBILIARE CON SUP. UTILE NETTA SUPERIORE AL LIMITE", 17))
                    cmbTipo.Items.Add(New ListItem("LIMITE PATRIMONIALE SUPERATO", 18))
                    cmbTipo.Items.Add(New ListItem(" LIMITE ISEE SUPERATO", 19))



                    cmbTipo.Items.Add(New ListItem("----", -1))
                    cmbTipo.Items.FindByValue(-1).Selected = True

                    'cmbBando.Items.FindByText("BANDO 2011").Selected = True
                    cmbBando.Items.FindByValue(id_bando).Selected = True

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If

        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If txtDal.Visible = True Then
            If txtDal.Text <> "" And txtAl.Text = "" Then
                Response.Write("<script>alert('Specificare entrambe le date o lasciare vuote entrambe le date!');</script>")
                Exit Sub
            End If
        End If
        Response.Redirect("RisultatoRicDom.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text.ToUpper) & "&NM=" & par.VaroleDaPassare(txtNome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(txtCF.Text.ToUpper) & "&PG=" & par.VaroleDaPassare(txtPG.Text.ToUpper) & "&ST=" & cmbStato.SelectedItem.Text & "&BA=" & cmbBando.SelectedItem.Value & "&LI=" & cmbDaLiquidare.SelectedItem.Value & "&MA=" & cmbMandato.SelectedItem.Value & "&OP=" & cmbOperatore.SelectedItem.Value & "&RE=" & cmbTipo.SelectedItem.Value & "&DAL=" & txtDal.Text & "&AL=" & txtAl.Text)
    End Sub

    Protected Sub cmbStato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStato.SelectedIndexChanged
        If cmbStato.SelectedItem.Text = "RESPINTE" Then
            cmbTipo.Visible = True
            Label11.Visible = True
            Label12.Visible = True
            Label13.Visible = True
            txtDal.Visible = True
            txtAl.Visible = True
        Else
            Label11.Visible = False
            cmbTipo.Visible = False
            Label12.Visible = False
            Label13.Visible = False
            txtDal.Visible = False
            txtAl.Visible = False
            cmbTipo.SelectedIndex = -1
            cmbTipo.Items.FindByValue(-1).Selected = True

        End If
    End Sub
End Class
