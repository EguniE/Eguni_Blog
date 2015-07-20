import javax.swing.JLabel;
import javax.swing.JFrame;
import javax.swing.JOptionPane;

public class HelloWorld extends JFrame {

    public static void main(String args[]) 
    {
     HelloWorld a = new HelloWorld();
     int[] myArray = { 4, 7, 5, 9, 2, 0, 1 };

     a.AddArray(myArray);
    }

    HelloWorld() {}
    {
      JLabel jlbHelloWorld = new JLabel("Hello World");
      add(jlbHelloWorld);
      this.setSize(100, 100);
      setVisible(true);
    }

    public void HelloWorldProcedure(String str) 
    { 
      JOptionPane.showMessageDialog(this, "Hello World by : ".concat(str), "Hello", JOptionPane.INFORMATION_MESSAGE, null );        
    }
       
    public int AddTwoNumbers(int FirstValue, int SecondValue, String str) 
    {     	   
      JOptionPane.showMessageDialog(this, "Hello ".concat(str) + "\n" +
    		                              "AddTwoNumbers with the parameters : " + Integer.toString( FirstValue ) +
    		                              " and "  + Integer.toString( SecondValue ) + " will return " +
    		                              Integer.toString( FirstValue + SecondValue ), 
    		                              "Hello", JOptionPane.INFORMATION_MESSAGE, null );
      return  FirstValue + SecondValue;
    }

    public int AddTwoNumbersFromArray(int[] val, String str) 
    {     	   
      JOptionPane.showMessageDialog(this, "Hello ".concat(str) + "\n" +
    		                              "AddTwoNumbers with the parameters : " + Integer.toString( val[0] ) +
    		                              " and "  + Integer.toString( val[1] ) + " will return " +
    		                              Integer.toString( val[0] + val[1] ), 
    		                              "Hello", JOptionPane.INFORMATION_MESSAGE, null );
      return  val[0] + val[1];
    }

    public void AddArray(int[] val) 
    {     	   
      JOptionPane.showMessageDialog(this, "Hello simon" + "\n" +
    		                              "AddTwoNumbers with the parameters : " + Integer.toString( val[0] ) +
    		                              " and "  + Integer.toString( val[1] ) + " will return " +
    		                              Integer.toString( val[0] + val[1] ), 
    		                              "Hello", JOptionPane.INFORMATION_MESSAGE, null );
    }

} 