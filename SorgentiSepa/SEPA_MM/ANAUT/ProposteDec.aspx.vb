
Partial Class ANAUT_ProposteDec
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label1.Text = "PROPOSTA DI DECADENZA DIC. PG. " & Request.QueryString("PG")
            Dim S As String = Request.QueryString("I")
            Try


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                Label2.Visible = False
                IDDIC.Value = Request.QueryString("ID")
                par.cmd.CommandText = "SELECT UTENZA_PROP_DECADENZA.* FROM UTENZA_PROP_DECADENZA WHERE UTENZA_PROP_DECADENZA.ID_DICHIARAZIONE=" & IDDIC.Value & " ORDER BY UTENZA_PROP_DECADENZA.ID DESC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    IDPROP.Value = myReader("ID")
                    If par.IfNull(myReader("M1"), "0") = "1" Then
                        ChM1.Checked = True
                    Else
                        ChM1.Checked = False
                    End If
                    If par.IfNull(myReader("M2"), "0") = "1" Then
                        ChM2.Checked = True
                    Else
                        ChM2.Checked = False
                    End If
                    If par.IfNull(myReader("M3"), "0") = "1" Then
                        ChM3.Checked = True
                    Else
                        ChM3.Checked = False
                    End If
                    If par.IfNull(myReader("M4"), "0") = "1" Then
                        ChM4.Checked = True
                    Else
                        ChM4.Checked = False
                    End If
                    If par.IfNull(myReader("M5"), "0") = "1" Then
                        ChM5.Checked = True
                    Else
                        ChM5.Checked = False
                    End If
                    If par.IfNull(myReader("M6"), "0") = "1" Then
                        ChM6.Checked = True
                    Else
                        ChM6.Checked = False
                    End If
                    If par.IfNull(myReader("M7"), "0") = "1" Then
                        ChM7.Checked = True
                    Else
                        ChM7.Checked = False
                    End If
                    If par.IfNull(myReader("M8"), "0") = "1" Then
                        ChM8.Checked = True
                    Else
                        ChM8.Checked = False
                    End If
                    If par.IfNull(myReader("M9"), "0") = "1" Then
                        ChM9.Checked = True
                    Else
                        ChM9.Checked = False
                    End If
                    If par.IfNull(myReader("M10"), "0") = "1" Then
                        ChM10.Checked = True
                    Else
                        ChM10.Checked = False
                    End If
                    If par.IfNull(myReader("M11"), "0") = "1" Then
                        ChM11.Checked = True
                    Else
                        ChM11.Checked = False
                    End If
                    If par.IfNull(myReader("M12"), "0") = "1" Then
                        ChM12.Checked = True
                    Else
                        ChM12.Checked = False
                    End If
                    If par.IfNull(myReader("M13"), "0") = "1" Then
                        ChM13.Checked = True
                    Else
                        ChM13.Checked = False
                    End If
                    If par.IfNull(myReader("M14"), "0") = "1" Then
                        ChM14.Checked = True
                    Else
                        ChM14.Checked = False
                    End If

                    txtData.Text = par.FormattaData(par.IfNull(myReader("DATA_PROPOSTA"), ""))
                    txtNote.Text = par.IfNull(myReader("NOTE"), "")

                    If S = "1" Then
                        ChM5.Checked = False
                        ChM5.Enabled = False
                    End If

                Else
                    par.cmd.CommandText = "INSERT INTO UTENZA_PROP_DECADENZA (ID,ID_DICHIARAZIONE) VALUES (SEQ_UTENZA_PROP_DECADENZA.NEXTVAL," & IDDIC.Value & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_UTENZA_PROP_DECADENZA.CURRVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        IDPROP.Value = myReader1(0)
                    End If
                    myReader1.Close()
                    ChM1.Checked = True
                    ChM2.Checked = True
                    ChM3.Checked = True
                    ChM4.Checked = True
                    ChM5.Checked = True
                    ChM6.Checked = True
                    ChM7.Checked = True
                    ChM8.Checked = True
                    ChM9.Checked = True
                    ChM10.Checked = True
                    ChM11.Checked = True
                    ChM12.Checked = True
                    ChM13.Checked = True
                    ChM14.Checked = True

                    If S = "1" Then
                        ChM5.Checked = False
                        ChM5.Enabled = False
                    End If
                End If
                myReader.Close()

                If Session.Item("PROP_DEC") = "0" Then
                    imgSalva.Visible = False
                    ChM1.Enabled = False
                    ChM2.Enabled = False
                    ChM3.Enabled = False
                    ChM4.Enabled = False
                    ChM5.Enabled = False
                    ChM6.Enabled = False
                    ChM7.Enabled = False
                    ChM8.Enabled = False
                    ChM9.Enabled = False
                    ChM10.Enabled = False
                    ChM11.Enabled = False
                    ChM12.Enabled = False
                    ChM13.Enabled = False
                    ChM14.Enabled = False
                    txtData.Enabled = False
                    txtNote.Enabled = False
                End If


            Catch ex As Exception
                Label2.Visible = True
                Label2.Text = ex.Message
            End Try
        End If

        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "UPDATE UTENZA_PROP_DECADENZA SET DATA_PROPOSTA='" & par.FormattaData(TXTDATA.TEXT) & "',NOTE='" & par.PulisciStrSql(TXTNOTE.TEXT) & "',M1='" & Valore01(CHM1.CHECKED) & "',M2='" & Valore01(CHM2.CHECKED) & "',M3='" & Valore01(CHM3.CHECKED) & "',M4='" & Valore01(CHM4.CHECKED) & "',M5='" & Valore01(CHM5.CHECKED) & "',M6='" & Valore01(CHM6.CHECKED) & "',M7='" & Valore01(CHM7.CHECKED) & "',M8='" & Valore01(CHM8.CHECKED) & "',M9='" & Valore01(CHM9.CHECKED) & "',M10='" & Valore01(CHM10.CHECKED) & "',M11='" & Valore01(CHM11.CHECKED) & "',M12='" & Valore01(CHM12.CHECKED) & "',M13='" & Valore01(CHM13.CHECKED) & "',M14='" & Valore01(CHM14.CHECKED) & "' WHERE ID=" & IDPROP.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & IDDIC.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F80','','I')"
            par.cmd.ExecuteNonQuery()


            Response.Write("<script>alert('Operazione Effettuata! Per rendere effettive le modifiche, premere il pulsante SALVA della dichiarazione!');</script>")

        Catch ex As Exception
            Label2.Visible = True
            Label2.Text = ex.Message
        End Try
    End Sub
End Class
