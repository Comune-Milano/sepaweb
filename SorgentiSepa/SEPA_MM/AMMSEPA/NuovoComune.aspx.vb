
Partial Class AMMSEPA_NuovoComune
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            PAR.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & motivazione & "')"
            PAR.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            Dim buono As Boolean = True

            par.OracleConn.Open()
            par.SettaCommand(par)

            If IsNumeric(txtCAP.Text) = False Then
                Response.Write("<script>alert('Il cap deve essere dun valore numerico!!');</script>")
                buono = False
            End If

            If Len(txtCAP.Text) <> 5 Then
                Response.Write("<script>alert('Il cap deve essere di 5 numeri!!');</script>")
                buono = False
            End If

            If IsNumeric(txtkm.Text) = False Then
                Response.Write("<script>alert('La distanza in km deve essere indicata con un numero intero!!');</script>")
                buono = False
            End If

            If InStr(txtkm.Text, ".") > 0 Then
                Response.Write("<script>alert('La distanza in km deve essere indicata con un numero intero!!');</script>")
                buono = False
            End If

            If InStr(txtkm.Text, ",") > 0 Then
                Response.Write("<script>alert('La distanza in km deve essere indicata con un numero intero!!');</script>")
                buono = False
            End If

            If IsNumeric(txtPopolazione.Text) = False Then
                Response.Write("<script>alert('La popolazione deve essere indicata con un numero intero!!');</script>")
                buono = False
            End If

            'If Val(txtkm.Text) >= 0 Then
            '    Response.Write("<script>alert('La distanza in km deve essere indicata con un numero intero!!');</script>")
            '    buono = False
            'End If

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE UPPER(COD)='" & UCase(par.PulisciStrSql(txtCod.Text)) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                Response.Write("<script>alert('Il codice catastale esiste già nell\'archivio!!');</script>")
                buono = False
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE UPPER(nome)='" & UCase(par.PulisciStrSql(txtComune.Text)) & "'"
            myReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                Response.Write("<script>alert('Il comune esiste già nell\'archivio!!');</script>")
                buono = False
            End If
            myReader.Close()
            If buono = True Then
                Dim entro As String = "0"
                If chEntro.Checked = True Then
                    entro = "1"
                End If
                par.cmd.CommandText = "INSERT INTO comuni_nazioni (ID,COD,SIGLA,NOME,CAP,ENTRO_70KM,DISTANZA_KM,POPOLAZIONE) VALUES (SEQ_COMUNI_NAZIONI.NEXTVAL,'" & UCase(par.PulisciStrSql(txtCod.Text)) & "','" & UCase(par.PulisciStrSql(txtProvincia.Text)) & "','" & UCase(par.PulisciStrSql(txtComune.Text)) & "','" & UCase(par.PulisciStrSql(txtCAP.Text)) & "','" & entro & "'," & txtkm.Text & "," & Val(txtPopolazione.Text) & ")"
                par.cmd.ExecuteNonQuery()
                Try
                    Dim operatore As String = Session.Item("ID_OPERATORE")
                    Eventi_Gestione(operatore, "F55", "INSERIMENTO NUOVO COMUNE " & txtComune.Text)
                Catch ex As Exception
                    lblErrore.Text = ex.Message
                    lblErrore.Visible = True
                End Try
                Response.Write("<script>alert('Operazione Effettuata!');</script>")
            End If
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'If Not IsPostBack Then


        '    par.OracleConn.Open()
        '    par.SettaCommand(par)

        '    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=61"
        '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        txtNumGiorni.Text = par.IfNull(myReader("VALORE"), "0")
        '    End If
        '    myReader.Close()

        '    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=62"
        '    myReader = par.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        txtLunghezza.Text = par.IfNull(myReader("VALORE"), "0")
        '    End If
        '    myReader.Close()

        '    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=63"
        '    myReader = par.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        If par.IfNull(myReader("VALORE"), "0") = "0" Then
        '            ChNumLet.Checked = False
        '        Else
        '            ChNumLet.Checked = True
        '        End If
        '    End If
        '    myReader.Close()


        '    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=64"
        '    myReader = par.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        txtTentativi.Text = par.IfNull(myReader("VALORE"), "0")
        '    End If
        '    myReader.Close()

        '    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=65"
        '    myReader = par.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        txtAttivita.Text = par.IfNull(myReader("VALORE"), "0")
        '    End If
        '    myReader.Close()

        '    par.OracleConn.Close()
        '    'End If
        'End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
