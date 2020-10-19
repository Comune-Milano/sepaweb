
Partial Class RicercaRinnovi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim ULTIMO_BAND0 As Integer = 0

            'txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            If cmbBando.Items.Count = 0 Then
                Dim lsiFrutto As New ListItem("TUTTI", "-2")
                cmbBando.Items.Add(lsiFrutto)

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT VALORE FROM PARAMETER WHERE ID=115"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    If par.IfNull(myReader1(0), "0") = "1" Then
                        par.cmd.CommandText = "select id from bandi order by id desc"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            myReader2.Read()
                            myReader2.Read()
                            myReader2.Read()
                            ULTIMO_BAND0 = myReader2(0)
                            Label3.Text = "La ricerca sarà effettuata all'interno dell'archivio ERP, ad esclusione delle domande di bando corrente e dei 2 semestri precedenti,  delle domande che sono in fase di assegnazione o verifica dei requisiti e delle domande escluse per Morte, Unifica, Assegnatario Bando POR, Rinuncia, Sopravvenuta Assegnazione e per decoorenza dei termini temporali di validità (DS)."
                        End If
                        myReader2.Close()
                    End If
                End If
                myReader1.Close()

                If ULTIMO_BAND0 = 0 Then
                    par.cmd.CommandText = "SELECT * FROM BANDI WHERE ID<>-1 and stato=2 ORDER BY ID ASC"
                Else
                    par.cmd.CommandText = "SELECT * FROM BANDI WHERE ID<>-1 and id<=" & ULTIMO_BAND0 & " ORDER BY ID ASC"
                End If
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                    cmbBando.Items.Add(lsiFrutto)
                End While
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'If txtPG.Text = "" Then
        ' Response.Write("<script>alert('Specificare un protocollo!');</script>")
        ' Else
        Response.Redirect("RisultatoRinnovi.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&PG=" & par.VaroleDaPassare(txtPG.Text) & "&BA=" & cmbBando.SelectedItem.Value)
        'End If
    End Sub

End Class
