﻿using System.ComponentModel.DataAnnotations;
using EFCore.data;
using EFCore.Controllers;
using EFCore.Model;
using EFCore.UI;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class App
{
    private Ui _ui = new();
    private readonly CommandCtrl _commandCtrl;
    private bool _running = false;

    public App()
    {
        _commandCtrl = new CommandCtrl(_ui);
    }

    public int Run()
    {
        _ui.Display("Welcome to FacilityDbManager\n" +
                    "Enter 'h' for help\n");
        _running = true;
        while (_running)
        {
            try
            {
                string command = _ui.GetCommand();
                HandleCommand(command);
            }
            catch (Exception e)
            {
                _ui.Display(e.Message + "\n");
            }   
        }

        return 0;
    }

    // Command handler
    private void HandleCommand(string command)
    {
        switch (command)
        {
            case "h":
            case "help":
            {
                _ui.DisplayHelp();
                break;
            }
            case "q":
            case "quit":
            {
                _ui.Display("Exiting application\n");
                _running = false;
                break;
            }
            case "a":
            case "add":
            {
                OnAdd();
                break;
            }
            case "g":
            case "get":
            {
                throw new NotImplementedException();
            }
            case "s":
            case "show":
            {
                throw new NotImplementedException();
            }
            case "get f":
            {
                OnGetFacilities();
                break;
            }
            case "get f ordered":
            {
                OnGetOrdered();
                break;
            }
            case "get reservations":
            {
                OnGetReservations();
                break;
            }
            case "reset database":
            {
                OnReset();
                break;
            }
            default:
                _ui.Display("Unrecognized command\n");
                break;
        }
    }

    private void OnReset()
    {
        _commandCtrl.DeleteAllData();
    }

    private void OnGetOrdered()
    {
        _commandCtrl.GetFacilitiesOrderByKind();
    }

    private void OnAdd()
    {
        string table = _ui.GetTable();
        switch (table)
        {
            case "f":
            case "Facility":
            case "facility":
            {
                _commandCtrl.UserEnterFacility();
                break;
            }
            default:
                _ui.Display("No such table exists!\n");
                break;
        }
    }

    private void OnGetFacilities()
    {
        _commandCtrl.GetAllFacilitiesWAddress();
    }

    private void OnGetReservations()
    {
        _commandCtrl.GetReservations();
    }
    
}