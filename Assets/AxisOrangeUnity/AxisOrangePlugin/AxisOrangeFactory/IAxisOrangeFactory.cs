namespace AxisOrange {
    public interface IAxisOrangeFactory {
        IAxisOrangeSensor CreateWithId(int id);
    }
}
