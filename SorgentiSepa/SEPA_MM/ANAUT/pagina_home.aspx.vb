
Partial Class ANAUT_pagina_home
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            Session.LCID = 1040
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                If Session.Item("LIVELLO") = "1" Then
                    Session.Add("ID_STRUTTURA", "-1")
                Else
                    par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read = True Then
                        Session.Add("ID_STRUTTURA", myReader("ID_UFFICIO"))
                    End If
                    myReader.Close()
                End If


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try

            'max 02/10/2015 nascondo, per ora non serve
            HyperLink3.Visible = False
            Image1.Visible = False
            Image2.Visible = False
            HyperLink4.Visible = False


            txtmessaggio.Value = Session.Item("ORARIO")
            Label3.Text = Session.Item("ORARIO")



        End If

        'par.OracleConn.Dispose()

    End Sub
End Class
