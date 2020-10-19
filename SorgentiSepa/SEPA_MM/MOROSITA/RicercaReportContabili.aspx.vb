'*** RICERCA REPORT CONTABILITA MOROSITA'

Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class MOROSITA_RicercaReportContabili
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If


        If Not IsPostBack Then

            'SettaggioCampi()


            Me.txtDataRIF_DAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRIF_AL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub



    'CARICO Campi e/o ComboBox
    Private Sub SettaggioCampi()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            'NOTA GIUSEPPE: selezionare solo le date per ID_MOROSITA not null 
            par.cmd.CommandText = "select MIN(RIF_DA) from SISCOM_MI.MOROSITA "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                'Me.txtDataDAL.Text = par.FormattaData(par.IfNull(myReader1(0), ""))
                'Me.txtDataAL.Text = par.FormattaData(Format(Now, "yyyyMMdd"))
            End If
            myReader1.Close()

            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function



    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim ControlloCampi As Boolean = True
        Dim TipoGestione As String
        Try

            ' & "   and RIFERIMENTO_DA>20090930 " _

            If RBList1.Items(1).Selected = True Then
                TipoGestione = "MG"
                'PRIMA DEL 30 settembre 2009 MG
                If Strings.Len(Me.txtDataRIF_AL.Text) > 0 Then
                    'Controllo che entrambi le date non superi 30 sett 2009

                    If par.AggiustaData(Me.txtDataRIF_AL.Text) > 20090930 Then
                        Response.Write("<script>alert('La data competenza deve essere precedente o uguale al 30/09/2009');</script>")
                        ControlloCampi = False
                        Exit Sub
                    End If

                    If Strings.Len(Me.txtDataRIF_DAL.Text) > 0 Then

                        If par.AggiustaData(Me.txtDataRIF_DAL.Text) > 20090930 Then
                            Response.Write("<script>alert('La data competenza deve essere precedente o uguale al 30/09/2009');</script>")
                            ControlloCampi = False
                            Exit Sub
                        End If


                        If par.AggiustaData(Me.txtDataRIF_DAL.Text) > par.AggiustaData(Me.txtDataRIF_AL.Text) Then
                            Response.Write("<script>alert('Attenzione...Controllare il range delle date!');</script>")
                            ControlloCampi = False
                            Exit Sub
                        End If
                    End If

                ElseIf Strings.Len(Me.txtDataRIF_DAL.Text) > 0 Then
                    If par.AggiustaData(Me.txtDataRIF_DAL.Text) > 20090930 Then
                        Response.Write("<script>alert('La data competenza deve essere precedente o uguale al 30/09/2009');</script>")
                        ControlloCampi = False
                        Exit Sub
                    End If
                End If
                '******************************************************************************
            Else
                TipoGestione = "MA"
                'DOPO IL 30 settembre 2009 MA
                If Strings.Len(Me.txtDataRIF_AL.Text) > 0 Then
                    'Controllo che entrambi le date non superi da data odierna e siano minori del 30 sett 2009

                    If Format(Now, "yyyyMMdd") < par.AggiustaData(Me.txtDataRIF_AL.Text) Then
                        Response.Write("<script>alert('La data competenza deve essere precedente o uguale alla data odierna');</script>")
                        ControlloCampi = False
                        Exit Sub
                    End If

                    If par.AggiustaData(Me.txtDataRIF_AL.Text) <= 20090930 Then
                        Response.Write("<script>alert('La data competenza deve essere successiva al 30/09/2009');</script>")
                        ControlloCampi = False
                        Exit Sub
                    End If

                    If Strings.Len(Me.txtDataRIF_DAL.Text) > 0 Then

                        If Format(Now, "yyyyMMdd") < par.AggiustaData(Me.txtDataRIF_DAL.Text) Then
                            Response.Write("<script>alert('La data competenza deve essere precedente o uguale alla data odierna');</script>")
                            ControlloCampi = False
                            Exit Sub
                        End If

                        If par.AggiustaData(Me.txtDataRIF_DAL.Text) <= 20090930 Then
                            Response.Write("<script>alert('La data competenza deve essere successiva al 30/09/2009');</script>")
                            ControlloCampi = False
                            Exit Sub
                        End If


                        If par.AggiustaData(Me.txtDataRIF_DAL.Text) > par.AggiustaData(Me.txtDataRIF_AL.Text) Then
                            Response.Write("<script>alert('Attenzione...Controllare il range delle date!');</script>")
                            ControlloCampi = False
                            Exit Sub
                        End If
                    End If

                ElseIf Strings.Len(Me.txtDataRIF_DAL.Text) > 0 Then

                    If Format(Now, "yyyyMMdd") < par.AggiustaData(Me.txtDataRIF_DAL.Text) Then
                        Response.Write("<script>alert('La data competenza deve essere precedente o uguale alla data odierna');</script>")
                        ControlloCampi = False
                        Exit Sub
                    End If

                    If par.AggiustaData(Me.txtDataRIF_DAL.Text) <= 20090930 Then
                        Response.Write("<script>alert('La data competenza deve essere successiva al 30/09/2009');</script>")
                        ControlloCampi = False
                        Exit Sub
                    End If

                End If
            End If


            If ControlloCampi = True Then

                ''height=580,top=0,left=0,width=780'

                Response.Write("<script>window.open('Flussi_Anno.aspx?DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataRIF_DAL.Text), "") _
                                                                   & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataRIF_AL.Text), "") _
                                                                   & "&TIPO=" & TipoGestione _
                                    & "','STAMPA" & Format(Now, "hhss") & "');</script>")

            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try





    End Sub
End Class
