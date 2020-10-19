
Partial Class Anagrafe
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Shared Indice As Long

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        'If Session.Item("ID_OPERATORE") <> "1" Then
        '    Me.btnAccetto.Visible = False
        'End If
        If Not IsPostBack Then
            Try

                Dim Tipo As Integer
                Dim ds As New Data.DataSet()
                Dim dlist As RadioButtonList
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter



                Tipo = CInt(Request.QueryString("T"))
                If Tipo <> "7" Then
                    Indice = CLng(PAR.DeCriptaMolto(Request.QueryString("ID")))
                    indice1.Value = Indice
                Else
                    Indice = CLng(Request.QueryString("ID"))
                End If

                indice1.Value = Indice

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn.Open()
                    PAR.SettaCommand(PAR)
                End If


                dlist = ListaOperatori
                Select Case Tipo
                    Case 0
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM comp_nucleo WHERE id_dichiarazione=" & Indice & " ORDER BY progr asc", PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "1"
                    Case 1
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM UTENZA_comp_nucleo WHERE id_dichiarazione=" & Indice & " ORDER BY progr asc", PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "2"
                    Case 2
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM comp_nucleo_CAMBI WHERE id_dichiarazione=" & Indice & " ORDER BY progr asc", PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "3"
                    Case 3
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM comp_nucleo_FSA WHERE id_dichiarazione=" & Indice & " ORDER BY progr asc", PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "4"
                    Case 4
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM comp_nucleo_vsa WHERE id_dichiarazione=" & Indice & " ORDER BY progr asc", PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "5"
                    Case 6
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali WHERE anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_contratto=" & Indice, PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "6"
                    Case 7
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID,cognome||' '||nome||' - '||cod_fiscale AS ""NOMINATIVO"",cod_fiscale FROM siscom_mi.anagrafica WHERE cod_fiscale='" & Request.QueryString("CF") & "' AND ANAGRAFICA.ID = (SELECT MAX(ID) FROM siscom_mi.ANAGRAFICA AN WHERE AN.COD_FISCALE=ANAGRAFICA.COD_FISCALE)", PAR.OracleConn)
                        da.Fill(ds)
                        chiamante.Value = "7"
                End Select


                dlist.Items.Clear()
                dlist.DataSource = ds
                dlist.DataTextField = "NOMINATIVO"
                dlist.DataValueField = "COD_FISCALE"
                dlist.DataBind()

                da.Dispose()
                da = Nothing

                dlist.DataSource = Nothing
                dlist = Nothing

                ds.Clear()
                ds.Dispose()
                ds = Nothing
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If ListaOperatori.Items.Count > 0 Then
                    ListaOperatori.Items(0).Selected = True
                End If
                If ListaOperatori.Items.Count = 1 Then
                    chiusura.Value = "1"
                Else
                    chiusura.Value = "0"
                End If

            Catch ex As Exception
                PAR.OracleConn.Close()
                Response.Write(ex.Message)
            End Try
        End If

    End Sub



    Protected Sub btnAccetto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAccetto.Click
        If ListaOperatori.Items.Count > 0 Then
            If Len(ListaOperatori.SelectedItem.Value) = 16 Then
                If Session.Item("ANAGRAFE_UTENZA_LIGHT") = "1" Then
                    Dim ImpostaChiave As New Fornitori
                    Dim Chiave As String = PAR.getPageId & "_" & Format(Now, "yyyyMMddHHmmss")
                    Dim esito As String = ImpostaChiave.ImpostaChiave(Chiave)
                    If esito = "1" Then
                        Response.Write("<script>window.open('https://sepa..../Anagrafica2.aspx?ID=" & PAR.CriptaMolto(Format(Now, "yyyyMMddHH") & "#" & Format(Indice, "000000000000000") & "@" & Chiave) & "&CF=" & PAR.Cripta(ListaOperatori.SelectedItem.Value) & "&OP=" & PAR.Cripta(Mid(Session.Item("operatore"), 1, 16)) & "&C=" & chiamante.Value & "','SIPO','');</script>")
                    Else
                        Response.Write("<p style='font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold'>Non è possibile effettuare il collegamento</p>")
                    End If
                Else
                    'Dim ImpostaChiave As New Fornitori
                    Dim Chiave As String = PAR.getPageId & "_" & Format(Now, "yyyyMMddHHmmss")
                    Try
                        PAR.OracleConn.Open()
                        PAR.SettaCommand(PAR)

                        PAR.cmd.CommandText = "INSERT INTO AU_LIGHT_CONCESSIONI VALUES ('" & Chiave & "')"
                        PAR.cmd.ExecuteNonQuery()
                        PAR.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Dim sCHIUDI As String = ""
                        If chiusura.Value = "1" Then
                            sCHIUDI = "self.close();"
                        End If
                        Response.Write("<script>window.open('Anagrafica3.aspx?ID=" & PAR.CriptaMolto(Format(Now, "yyyyMMddHH") & "#" & Format(CDbl(indice1.Value), "000000000000000") & "@" & Chiave) & "&CF=" & PAR.Cripta(ListaOperatori.SelectedItem.Value) & "&OP=" & PAR.Cripta(Mid(Session.Item("operatore"), 1, 16)) & "&C=" & chiamante.Value & "','SIPO','');" & sCHIUDI & "</script>")
                    Catch ex As Exception
                        PAR.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        Response.Write("<p style='font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold'>Non è possibile effettuare il collegamento</p>")
                    End Try
                   
                    'Response.Write("<script>window.open('Anagrafe1.aspx?ID=" & Indice & "&CF=" & PAR.Cripta(ListaOperatori.SelectedItem.Value) & "&C=" & chiamante.Value & "','SIPO','');</script>")
                End If
            End If
        End If
    End Sub


End Class
