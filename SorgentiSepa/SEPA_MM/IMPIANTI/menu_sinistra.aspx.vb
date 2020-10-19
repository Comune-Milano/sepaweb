Imports System.Collections

Partial Class ANAUT_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Data1 As String
        Dim GiorniTrascorsi As Integer
        Dim GiorniPreAllarme As Integer
        Dim tn As TreeNode
        Dim i, Livello As Integer

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try



            If Not HttpContext.Current.Session.Item("SESSION_IMPIANTI") Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("SESSION_IMPIANTI"), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Remove("SESSION_IMPIANTI")
            End If

            If Not IsPostBack Then
                Label3.Text = Format(Now(), "dd/MM/yyyy")


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                Me.ImageAllarme.ImageUrl = "../IMPIANTI/Immagini/AllarmeVERDE.png"

                par.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI_VERIFICHE where FL_STORICO='N'"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read

                    Data1 = par.IfNull(myReader1("DATA_SCADENZA"), "")
                    If Strings.Len(Data1) > 0 Then
                        GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(par.FormattaData(Data1)), CDate(Now.ToString("dd/MM/yyyy")))

                        GiorniPreAllarme = par.IfNull(myReader1("MESI_PREALLARME"), 1) * 30

                        If GiorniTrascorsi > 0 Then
                            ' ROSSO
                            Me.ImageAllarme.ImageUrl = "../IMPIANTI/Immagini/AllarmeROSSO.png"
                            'Response.Write("<script>parent.contents.Form1.ImageAllarme.src='../Immagini/AllarmeVERDE.gif';</script>")
                            Exit While
                        ElseIf GiorniTrascorsi >= -GiorniPreAllarme And GiorniTrascorsi <= 0 Then
                            ' pre allarme GIALLO  mesi prima la scadenza
                            Me.ImageAllarme.ImageUrl = "../IMPIANTI/Immagini/AllarmeGIALLO.png"
                        End If
                    End If
                End While

                myReader1.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



                ' CONTROLLO UTENTE
                If Session.Item("MOD_DEM_SL") = "1" Then
                    'SOLO LETTURA
                    tn = T1.FindNode("Impianti/1")

                    If Not IsNothing(tn) Then
                        T1.Nodes(0).ChildNodes.Remove(tn)
                    End If
                Else
                    'ABILITATO PER UNO o PIU' IMPIANTI
                    For i = 1 To 10
                        Livello = Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000"), i, 1)

                        Select Case i
                            Case 1
                                tn = T1.FindNode("Impianti/1/EL")
                            Case 2
                                tn = T1.FindNode("Impianti/1/ID")
                            Case 3
                                tn = T1.FindNode("Impianti/1/TE")
                            Case 4
                                tn = T1.FindNode("Impianti/1/TA")
                            Case 5
                                tn = T1.FindNode("Impianti/1/TR")
                            Case 6
                                tn = T1.FindNode("Impianti/1/SO")
                            Case 7
                                tn = T1.FindNode("Impianti/1/ME")
                            Case 8
                                tn = T1.FindNode("Impianti/1/AN")
                            Case 9
                                tn = T1.FindNode("Impianti/1/TU")
                            Case 10
                                tn = T1.FindNode("Impianti/1/CF")
                        End Select

                        If Not IsNothing(tn) And Livello = 0 Then
                            T1.Nodes(0).ChildNodes(0).ChildNodes.Remove(tn)
                        End If
                    Next i
                End If

            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("LAVORAZIONE") = "1" Then
            'Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Response.Write("<script>alert('Chiudere la maschera utilizzando la funzione Esci')</script>")
            Exit Sub
        End If



        Select Case T1.SelectedValue

            Case "ME"   ' IMPIANTI SOLLEVAMENTO ACQUE METEORICHE
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_Meteoriche.aspx?TIPO=1');</script>")

            Case "AN"   ' IMPIANTI ANTINCENDIO
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_Antincendio.aspx?TIPO=1');</script>")

            Case "CF"   ' IMPIANTI CANNA FUMARIA
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_CannaFumaria.aspx?TIPO=1');</script>")

            Case "EL"   ' IMPIANTO ELETTRICO
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "ID")
                Response.Write("<script>parent.main.location.replace('Imp_Elettrico.aspx?TIPO=1');</script>")

            Case "ID"   ' CENTRALE IDRICA
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "ID")
                Response.Write("<script>parent.main.location.replace('Imp_Idrico.aspx?TIPO=1');</script>")

            Case "SO"
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "SO")
                Response.Write("<script>parent.main.location.replace('Imp_Sollevamento.aspx?TIPO=1');</script>")

            Case "TR" ' IMPIANTI TELERISCALDAMENTO
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_Teleriscaldamento.aspx?TIPO=1');</script>")

            Case "TA"   ' IMPIANTI TERMICI AUTONOMI
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_RiscaldamentoA.aspx?TIPO=1');</script>")

            Case "TE"   ' IMPIANTI TERMICI CENTRALIZZATI
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_Riscaldamento.aspx?TIPO=1');</script>")

            Case "TU"   ' IMPIANTI TUTELA IMMOBILE
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Imp_Tutela.aspx?TIPO=1');</script>")

            Case "GA"   ' IMPIANTO GAS
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "ID")
                Response.Write("<script>parent.main.location.replace('Imp_Gas.aspx?TIPO=1');</script>")

            Case "CI"   ' IMPIANTO CITOFONICO
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "ID")
                Response.Write("<script>parent.main.location.replace('Imp_Citofonico.aspx?TIPO=1');</script>")

            Case "TV"   ' IMPIANTO TV
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "ID")
                Response.Write("<script>parent.main.location.replace('Imp_TV.aspx?TIPO=1');</script>")


            Case 2
                Response.Write("<script>parent.main.location.replace('RicercaImpianti.aspx');</script>")


        End Select
        T1.Nodes(0).Selected = True
    End Sub

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property


    Protected Sub TVerifiche_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TVerifiche.SelectedNodeChanged
        If Session.Item("LAVORAZIONE") = "1" Then
            'Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Response.Write("<script>alert('Chiudere la maschera utilizzando la funzione Esci')</script>")
            Exit Sub
        End If


        Select Case TVerifiche.SelectedValue


            Case "Verifiche"
                Response.Write("<script>parent.main.location.replace('RicercaVerifiche.aspx');</script>")


        End Select
        TVerifiche.Nodes(0).Selected = True

    End Sub

    Protected Sub T1_TreeNodeExpanded(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles T1.TreeNodeExpanded
        If e.Node.Parent Is Nothing Then
            For Each node As TreeNode In (CType(sender, TreeView)).Nodes
                If Not (node.Equals(e.Node)) Then
                    node.Collapse()
                End If
            Next
            Return
        End If

        Dim tn As TreeNode = e.Node.Parent
        For Each node As TreeNode In tn.ChildNodes
            If Not (node.Equals(e.Node)) Then
                node.Collapse()
            End If
        Next
    End Sub
End Class
