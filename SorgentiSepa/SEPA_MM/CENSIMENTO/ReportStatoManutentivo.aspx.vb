
Partial Class CENSIMENTO_ReportStatoManutentivo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Label11.Text = Session.Item("INDIRIZZOUNITA")
                Label13.Text = ""

                If Request.QueryString("TIPO") = "0" Then

                    par.cmd.CommandText = "select TAB_QUARTIERI.NOME AS QUARTIERE,unita_STATO_MANUTENTIVO.*,T_TIPO_ALL_ERP.DESCRIZIONE AS ""TIPOLOGIA"" from SISCOM_MI.TAB_QUARTIERI,T_TIPO_ALL_ERP,siscom_mi.unita_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.ID_QUARTIERE=TAB_QUARTIERI.ID (+) AND unita_STATO_MANUTENTIVO.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ID_UNITA=" & Request.QueryString("id")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then

                        Label22.Text = par.IfNull(myReader("QUARTIERE"), "")

                        Label15.Text = par.IfNull(myReader("ZONA"), "")
                        Label16.Text = par.IfNull(myReader("TIPOLOGIA"), "")
                        Label17.Text = par.IfNull(myReader("NUM_LOCALI"), "")
                        Label18.Text = par.IfNull(myReader("NUM_SERVIZI"), "")

                        Label2.Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))
                        Label1.Text = par.FormattaData(par.IfNull(myReader("DATA_PRE_SLOGGIO"), ""))

                        Label19.Text = par.FormattaData(par.IfNull(myReader("DATA_consegna_chiavi"), ""))
                        Label21.Text = par.FormattaData(par.IfNull(myReader("DATA_ripresa_chiavi"), ""))
                        Label20.Text = par.IfNull(myReader("consegnate_a"), "")

                        If par.IfNull(myReader("rilevazione"), 0) = "0" Then
                            Label3.Text = "NO"
                        Else
                            Label3.Text = "SI"
                        End If

                        If par.IfNull(myReader("riassegnabile"), 0) = "0" Then
                            Label4.Text = "SI"
                        Else
                            Label4.Text = "NO"
                        End If

                        If par.IfNull(myReader("p_blindata"), 0) = "0" Then
                            Label5.Text = "NO"
                        Else
                            Label5.Text = "SI"
                        End If

                        If par.IfNull(myReader("handicap"), 0) = "0" Then
                            Label6.Text = "SI"
                        Else
                            Label6.Text = "NO"
                        End If

                        Label7.Text = par.IfNull(myReader("note"), "")

                        Label8.Text = par.FormattaData(par.IfNull(myReader("DATA_pre_S"), ""))

                        Label14.Text = par.IfNull(myReader("MOTIVAZIONI"), "")

                        Dim SICUREZZA As String = ""

                        If par.IfNull(myReader("SOL_GP"), 0) = "1" Then
                            SICUREZZA = "GRATA PORTE"
                        End If

                        If par.IfNull(myReader("SOL_GF"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "GRATA FINESTRE"
                        End If

                        If par.IfNull(myReader("SOL_PB"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "PORTA BLINDATA"
                        End If


                        If par.IfNull(myReader("ALLARME"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "ALLARME"
                        End If

                        If par.IfNull(myReader("SOL_LA"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "LASTRATO"
                        End If

                        If par.IfNull(myReader("SOL_AL"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "ALTRO"
                        End If

                        If SICUREZZA <> "" Then
                            Label13.Text = "Opere previste per la messa in sicurezza: " & SICUREZZA
                        Else
                            Label13.Text = ""
                        End If

                    Else
                        Response.Write("Salvare lo stato manutentivo prima di stampare!")
                    End If
                    myReader.Close()
                    Label9.Text = ""
                    par.cmd.CommandText = "SELECT TIPO_INTERVENTI_MANU_UI.descrizione FROM siscom_mi.TIPO_INTERVENTI_MANU_UI,SISCOM_MI.UNITA_STATO_MANUTENTIVO_INT WHERE TIPO_INTERVENTI_MANU_UI.ID=unita_stato_manutentivo_int.id_intervento and unita_stato_manutentivo_int.ID_UNITA=" & Request.QueryString("ID")
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader2.Read
                        Label9.Text = Label9.Text & par.IfNull(myReader2("descrizione"), "") & "</br>"
                    Loop
                    myReader2.Close()
                    Label12.Text = Format(Now, "dd/MM/yyyy")
                Else
                    par.cmd.CommandText = "select TAB_QUARTIERI.NOME AS QUARTIERE,UNITA_STATO_MAN_S.*,T_TIPO_ALL_ERP.DESCRIZIONE AS ""TIPOLOGIA"" from SISCOM_MI.TAB_QUARTIERI,T_TIPO_ALL_ERP,siscom_mi.unita_STATO_MAN_S WHERE UNITA_STATO_MANUTENTIVO.ID_QUARTIERE=TAB_QUARTIERI.ID (+) AND unita_STATO_MAN_S.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ID_UNITA=" & Request.QueryString("id") & " and data_memo='" & Request.QueryString("DATA") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then

                        Label22.Text = par.IfNull(myReader("QUARTIERE"), "")

                        Label15.Text = par.IfNull(myReader("ZONA"), "")
                        Label16.Text = par.IfNull(myReader("TIPOLOGIA"), "")
                        Label17.Text = par.IfNull(myReader("NUM_LOCALI"), "")
                        Label18.Text = par.IfNull(myReader("NUM_SERVIZI"), "")

                        Label2.Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))
                        Label1.Text = par.FormattaData(par.IfNull(myReader("DATA_PRE_SLOGGIO"), ""))

                        Label19.Text = par.FormattaData(par.IfNull(myReader("DATA_consegna_chiavi"), ""))
                        Label21.Text = par.FormattaData(par.IfNull(myReader("DATA_ripresa_chiavi"), ""))
                        Label20.Text = par.IfNull(myReader("consegnate_a"), "")

                        If par.IfNull(myReader("rilevazione"), 0) = "0" Then
                            Label3.Text = "NO"
                        Else
                            Label3.Text = "SI"
                        End If

                        If par.IfNull(myReader("riassegnabile"), 0) = "0" Then
                            Label4.Text = "NO"
                        Else
                            Label4.Text = "SI"
                        End If

                        If par.IfNull(myReader("p_blindata"), 0) = "0" Then
                            Label5.Text = "NO"
                        Else
                            Label5.Text = "SI"
                        End If

                        If par.IfNull(myReader("handicap"), 0) = "0" Then
                            Label6.Text = "SI"
                        Else
                            Label6.Text = "NO"
                        End If

                        Dim SICUREZZA As String = ""

                        If par.IfNull(myReader("SOL_GP"), 0) = "1" Then
                            SICUREZZA = "GRATA PORTE"
                        End If

                        If par.IfNull(myReader("SOL_GF"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "GRATA FINESTRE"
                        End If

                        If par.IfNull(myReader("SOL_PB"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "PORTA BLINDATA"
                        End If


                        If par.IfNull(myReader("ALLARME"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "ALLARME"
                        End If

                        If par.IfNull(myReader("SOL_LA"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "LASTRATO"
                        End If

                        If par.IfNull(myReader("SOL_AL"), 0) = "1" Then
                            If SICUREZZA <> "" Then SICUREZZA = SICUREZZA & ", "
                            SICUREZZA = SICUREZZA & "ALTRO"
                        End If

                        If SICUREZZA <> "" Then
                            Label13.Text = "Opere previste per la messa in sicurezza: " & SICUREZZA
                        Else
                            Label13.Text = ""
                        End If

                        Label7.Text = par.IfNull(myReader("note"), "")

                        Label8.Text = par.FormattaData(par.IfNull(myReader("DATA_pre_S"), ""))
                        Label12.Text = par.FormattaData(par.IfNull(myReader("DATA_MEMO"), ""))
                        Label14.Text = par.IfNull(myReader("MOTIVAZIONI"), "")

                    End If
                    myReader.Close()
                    Label9.Text = ""
                    par.cmd.CommandText = "SELECT TIPO_INTERVENTI_MANU_UI.descrizione FROM siscom_mi.TIPO_INTERVENTI_MANU_UI,SISCOM_MI.UNITA_STATO_MAN_INT_s WHERE TIPO_INTERVENTI_MANU_UI.ID=unita_stato_man_int_s.id_intervento and unita_stato_man_int_s.ID_UNITA=" & Request.QueryString("ID") & " AND UNITA_STATO_MAN_INT_S.DATA_MEMO='" & Request.QueryString("DATA") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader2.Read
                        Label9.Text = Label9.Text & par.IfNull(myReader2("descrizione"), "") & "</br>"
                    Loop
                    myReader2.Close()

                End If

                par.OracleConn.Close()


            Catch ex As Exception
                par.OracleConn.Close()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
End Class
