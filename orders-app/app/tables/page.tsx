export default function Index() {
  return (
    <div className="flex-1 flex flex-col bg-white rounded-r-lg">
      <div className="p-5">
        <h1 className="text-xl font-bold">Wybierz stolik</h1>
        <div className="flex space-x-4 mt-4">
          <button className="px-4 py-2 bg-[#1E2A38] text-white rounded-md hover:bg-[#2B4B5C]">
            Bar
          </button>
        </div>
      </div>
    </div>
  );
}
