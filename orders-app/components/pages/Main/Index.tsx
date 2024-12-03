import Categories from "@/components/Categories/Index";
import MenuGrid from "@/components/MenuGrid/Index";
import OrderSummary from "@/components/OrderSummary/Index";
import Sidebar from "@/components/Sidebar/Index";

const MainPage = () => (
        <div className="w-4/5 h-[80vh] bg-white shadow-lg rounded-lg flex">
            <Sidebar/>
            <div className="flex flex-1">
                <OrderSummary/>
                <MenuGrid/>
                <Categories/>
            </div>
        </div>
);

export default MainPage;