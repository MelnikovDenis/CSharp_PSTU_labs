if(RecognizingAutomaton.IsWord(Console.ReadLine()))
      Console.WriteLine("+");
else
      Console.WriteLine("-");
Console.ReadKey();
public static class RecognizingAutomaton
{      
      public static bool IsWord(string str)
      {
            string state = "S";            
            int i = 0;
            do
            {
                  Console.WriteLine(state);
                  switch(state)
                  {
                        case "S":
                              if(str[i] == '_')
                                    state = "N";
                              else
                                    state = "E";
                        break;
                        case "N":
                              if(str[i] == '-')
                                    state = "L";
                              else if (str[i] == '+')
                                    state = "M";
                              else
                                    state = "E";
                        break;
                        case "M":
                              if(str[i] == 'b')
                                    state = "KN";
                              else if (str[i] == '-')
                                    state = "L";
                              else
                                    state = "E";
                        break;
                        case "L":
                              if(str[i] == 'a')
                                    state = "KN";                              
                              else
                                    state = "E";
                        break;
                        case "KN":
                              if(str[i] == '+')
                                    state = "M";
                              else if (str[i] == '-')
                                    state = "L";
                              else
                                    state = "E";
                        break;
                  }
                  ++i;
            }while(state != "E" && i < str.Length);
            if(state == "KN")
                  return true;
            else
                  return false;
      }
}