
Partial Class Contratti_SceltaContraenteUD
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Public Property Unita() As String
        Get
            If Not (ViewState("par_Unita") Is Nothing) Then
                Return CStr(ViewState("par_Unita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Unita") = value
        End Set

    End Property

    Public Property CODICEUnita() As String
        Get
            If Not (ViewState("par_CODICEUnita") Is Nothing) Then
                Return CStr(ViewState("par_CODICEUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CODICEUnita") = value
        End Set

    End Property

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Add("CONTRATTOAPERTO", "0")
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then

                Unita = Request.QueryString("U")
                CODICEUnita = Request.QueryString("CODICE")
                chiamante = Request.QueryString("T")

                Session.Add("CONTRATTOAPERTO", "1")

                If Chiamante = "4" Then
                    chFisica.Enabled = False
                End If
            End If




        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Public Property Chiamante() As String
        Get
            If Not (ViewState("par_Chiamante") Is Nothing) Then
                Return CStr(ViewState("par_Chiamante"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Chiamante") = value
        End Set

    End Property

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try



            lblErrore.Visible = False

            If prosegui.Value = "1" Then

                If chFisica.Checked = False And chGiuridica.Checked = False Then

                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Selezionare la tipologia di contraente"

                    Exit Sub

                End If

                Dim t As String = ""

                Select Case chiamante
                    Case "0"
                        t = "0"
                    Case "1"
                        t = "1"
                    Case "2"
                        t = "2"
                    Case "3"
                        t = "3"
                    Case "4"
                        t = "4"
                End Select

                If chFisica.Checked = True Then
                    Response.Redirect("ContraenteF.aspx?T=" & t & "&U=" & Unita & "&CODICE=" & CODICEUnita)
                Else
                    Response.Redirect("ContraenteG.aspx?T=" & t & "&U=" & Unita & "&CODICE=" & CODICEUnita)
                End If



            End If

        Catch ex As Exception

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
