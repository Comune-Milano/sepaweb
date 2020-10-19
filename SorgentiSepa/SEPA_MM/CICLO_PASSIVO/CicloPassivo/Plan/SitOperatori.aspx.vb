
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_SitOperatori
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("IDP")
            CaricaTabella()
        End If
    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from siscom_mi.t_esercizio_finanziario,siscom_mi.pf_main where pf_main.id=" & idPianoF.Value & " and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                TestoPagina = "<table style='width:100%;'>"
                TestoPagina = TestoPagina & "<tr><td align='center' style='font-family: arial; font-size: 12pt; font-weight: bold'>Piano Finanziario " & par.FormattaData(myreader1("inizio")) & " - " & par.FormattaData(myreader1("fine")) & " - Associazione Voci/Operatori</td></tr>"
                TestoPagina = TestoPagina & "</table></br>"
            End If
            myReader1.Close()

            TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='10%'>OPERATORE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='45%'>FUNZIONI</td><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='45%'>VOCI</td></tr>"

            Dim Funzioni As String = ""
            Dim Voci As String = ""

            par.cmd.CommandText = "select * from operatori where id in (select id_operatore from siscom_mi.pf_voci_operatori,siscom_mi.pf_voci where pf_voci.id_piano_finanziario=" & idPianoF.Value & " and pf_voci_operatori.id_voce=pf_voci.id) order by cognome asc,nome asc"
            myReader1 = par.cmd.ExecuteReader()
            Do While myReader1.Read

                If par.IfNull(myReader1("BP_FORMALIZZAZIONE"), "0") = "1" Then
                    Funzioni = "BP-Formalizzazione</br>"
                End If
                If par.IfNull(myReader1("BP_FORMALIZZAZIONE_L"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Formalizzazione (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_COMPILAZIONE"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Compilazione</br>"
                End If
                If par.IfNull(myReader1("BP_COMPILAZIONE_L"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Compilazione (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_CONV_ALER"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Convalida Gestore</br>"
                End If
                If par.IfNull(myReader1("BP_CONV_ALER_L"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Convalida Gestore (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_CAPITOLI"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Assegn. Capitoli</br>"
                End If

                If par.IfNull(myReader1("BP_CAPITOLI_L"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Assegn. Capitoli (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_CONV_COMUNE"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Convalida Comune</br>"
                End If
                If par.IfNull(myReader1("BP_CONV_COMUNE_L"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Convalida Comune (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_VOCI_SERVIZI"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Gestione Voci Servizi</br>"
                End If
                If par.IfNull(myReader1("BP_VOCI_SERVIZI_L"), "0") = "1" Then
                    Funzioni = Funzioni & "BP-Gestione Voci Servizi (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_MS"), "0") = "1" Then
                    Funzioni = Funzioni & "Manutenzioni e Servizi</br>"
                End If
                If par.IfNull(myReader1("BP_MS_L"), "0") = "1" Then
                    Funzioni = Funzioni & "Manutenzioni e Servizi (solo lettura)</br>"
                End If


                If par.IfNull(myReader1("BP_OP"), "0") = "1" Then
                    Funzioni = Funzioni & "Ordini e Pagamenti</br>"
                End If
                If par.IfNull(myReader1("BP_OP_L"), "0") = "1" Then
                    Funzioni = Funzioni & "Ordini e Pagamenti (solo lettura)</br>"
                End If


                If par.IfNull(myReader1("BP_PC"), "0") = "1" Then
                    Funzioni = Funzioni & "Pagamenti a Canone</br>"
                End If
                If par.IfNull(myReader1("BP_PC_L"), "0") = "1" Then
                    Funzioni = Funzioni & "Pagamenti a Canone (solo lettura)</br>"
                End If


                If par.IfNull(myReader1("BP_LO"), "0") = "1" Then
                    Funzioni = Funzioni & "Lotti</br>"
                End If
                If par.IfNull(myReader1("BP_LO_L"), "0") = "1" Then
                    Funzioni = Funzioni & "Lotti (solo lettura)</br>"
                End If

                If par.IfNull(myReader1("BP_CC"), "0") = "1" Then
                    Funzioni = Funzioni & "Contratti</br>"
                End If
                If par.IfNull(myReader1("BP_CC_L"), "0") = "1" Then
                    Funzioni = Funzioni & "Contratti (solo lettura)</br>"
                End If


                If Funzioni = "" Then
                    Funzioni = "Nessuna funzione associata"
                End If


                par.cmd.CommandText = "select pf_voci.descrizione from siscom_mi.pf_voci,siscom_mi.pf_voci_operatori where pf_voci.id_piano_finanziario=" & idPianoF.Value & " and pf_voci_operatori.id_voce=pf_voci.id and pf_voci_operatori.id_operatore=" & par.IfNull(myReader1("id"), -1)
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader2.Read
                    Voci = Voci & "- " & par.IfNull(myReader2("descrizione"), "") & ";</br>"
                Loop
                myReader2.Close()

                If Voci = "" Then
                    Voci = "Nessuna voce associata"
                End If

                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 8pt'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='10%'>" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Funzioni & "</td><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Voci & "</td></tr>"

                Voci = ""
                Funzioni = ""



            Loop
            myReader1.Close()
            TestoPagina = TestoPagina & "</table></br>"



            Response.Write(TestoPagina)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Function
End Class
